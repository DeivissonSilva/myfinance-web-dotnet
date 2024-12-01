using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using myfinance_web_dotnet.App_Code_Clean.Core.Services;
using myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces;
using myfinance_web_dotnet.Models;
using System.Diagnostics;

namespace myfinance_web_dotnet.Controllers
{
    public class HistoricoTransacoesController : Controller
    {
        private readonly ILogger<HistoricoTransacoesController> _logger;
        private readonly IHistoricoTransacoesServices _historicoTransacoesServices;
        private readonly IPlanoContasServices _planoContasServices;

        public HistoricoTransacoesController(ILogger<HistoricoTransacoesController> logger,
            IHistoricoTransacoesServices historicoTransacoesServices,
            IPlanoContasServices planoContasServices)
        {
            _logger = logger;
            _historicoTransacoesServices = historicoTransacoesServices;
            _planoContasServices = planoContasServices;
        }

        public async Task<IActionResult> Index()
        {
            //Poderia usar autoMap,mas como é um sistema pequeno e academico fica para futuras implementações.

            var _historicoTransacoes = await _historicoTransacoesServices.ObterTodosHistoricoTransacoesAsync();
            List<HistoricoTransacoesModel> registros = [];

            _historicoTransacoes.ForEach(item =>
            {
                var HistoricoTransacoesModel = new HistoricoTransacoesModel
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Data = item.Data,
                    Valor = item.Valor,
                    PlanoContaId = item.PlanoContaId,
                    PlanoContas = item.PlanoContasDto

                };

                registros.Add(HistoricoTransacoesModel);
            });

            return View(registros);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new HistoricoTransacoesModel
            {
                Data = DateTime.Now
            };

            await CarregarPlanoContas();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HistoricoTransacoesModel historicoTransacoesModelCreate)
        {
            ModelState.Remove("PlanoContas");

            if (!ModelState.IsValid)
            {
                await CarregarPlanoContas();

                return View(historicoTransacoesModelCreate);
            }


            if (ModelState.IsValid)
            {
                var novaTransacao = new HistoricoTransacoesDto
                {
                    Descricao = historicoTransacoesModelCreate.Descricao,
                    Data = historicoTransacoesModelCreate.Data,
                    Valor = (historicoTransacoesModelCreate.Valor / 100),
                    PlanoContaId = historicoTransacoesModelCreate.PlanoContaId,
                };

                await _historicoTransacoesServices.AdicionarHistoricoTransacoesAsync(novaTransacao);

                return RedirectToAction(nameof(Index));
            }

            return View(historicoTransacoesModelCreate);
        }

        private async Task CarregarPlanoContas()
        {
            var planoContas = await _planoContasServices.ObterTodosPlanosContasAsync();
            planoContas = planoContas.OrderBy(item => item.Descricao).ToList();
            ViewBag.PlanoContaList = new SelectList(planoContas, "Id", "Descricao");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoTransacao = await _historicoTransacoesServices.ObterHistoricoTransacaoPorIdAsync((int)id);

            if (historicoTransacao == null)
            {
                return NotFound();
            }

            await CarregarPlanoContas();

            var historicoTransacoesModelEdit = ObterHistoricoTransacoesModel(historicoTransacao);

            return View(historicoTransacoesModelEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HistoricoTransacoesModel HistoricoTransacoesModel)
        {
            ModelState.Remove("PlanoContas");

            if (id != HistoricoTransacoesModel.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var historicoTransacoesModel = ObterPlanoContaDto(HistoricoTransacoesModel);
                historicoTransacoesModel.Valor = (historicoTransacoesModel.Valor / 100);
                await _historicoTransacoesServices.AtualizarHistoricoTransacoesAsync(historicoTransacoesModel);

                return RedirectToAction(nameof(Index));
            }

            await CarregarPlanoContas();

            return View(HistoricoTransacoesModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoTransacoesDto = await _historicoTransacoesServices.ObterHistoricoTransacaoPorIdAsync((int)id);

            if (historicoTransacoesDto == null)
            {
                return NotFound();
            }

            var HistoricoTransacoesModel = ObterHistoricoTransacoesModel(historicoTransacoesDto);

            return View(HistoricoTransacoesModel);
        }

        [ActionName("DeleteTransacaoConfirmed")]
        [HttpPost]
        public async Task<IActionResult> DeleteTransacaoConfirmed(int Id)
        {
            var historicoTransacoesDto = await _historicoTransacoesServices.ObterHistoricoTransacaoPorIdAsync(Id);

            if (historicoTransacoesDto != null)
            {
                await _historicoTransacoesServices.RemoverHistoricoTransacoesAsync(historicoTransacoesDto);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Relatorio()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> RelatorioPorPeriodo(DateTime? dataInicio, DateTime? dataFim, string acao)
        {
            if (!dataInicio.HasValue || !dataFim.HasValue)
            {
                ModelState.AddModelError(string.Empty, "As datas de início e fim são obrigatórias.");
            }

            if (dataInicio > dataFim)
            {
                ModelState.AddModelError(string.Empty, "A data de início não pode ser maior que a data de fim.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.DataInicio = dataInicio;
                ViewBag.DataFim = dataFim;
                return View("Relatorio");
            }

            ViewBag.DataInicio = dataInicio;
            ViewBag.DataFim = dataFim;

            var dataFimdate = Convert.ToDateTime(dataFim);
            var dataIniciodate = Convert.ToDateTime(dataInicio);
            dataFimdate = dataFimdate.Date.AddDays(1).AddMilliseconds(-1);
            var transacoes = await _historicoTransacoesServices.ObterTransacoesPorPeriodo(dataIniciodate, dataFimdate);
            List<HistoricoTransacoesModel> registros = [];

            var totalReceita = transacoes.Where(x=> x.PlanoContasDto.Tipo.Equals("R")).Sum(x => x.Valor);
            var totalDespesa = transacoes.Where(x => x.PlanoContasDto.Tipo.Equals("D")).Sum(x => x.Valor);
            var totalGeral = totalReceita - totalDespesa;

            @ViewData["totalReceita"] = totalReceita.ToString("C2");
            @ViewData["totalDespesa"] = totalDespesa.ToString("C2");
            @ViewData["totalGeral"] = totalGeral.ToString("C2");
           
            if (acao == null)
            {
                return BadRequest("Ação inválida.");
            }

            if (acao.Equals("Relatorio"))
            {
                transacoes.ForEach(item =>
                {
                    var HistoricoTransacoesModel = new HistoricoTransacoesModel
                    {
                        Id = item.Id,
                        Descricao = item.Descricao,
                        Data = item.Data,
                        Valor = item.Valor,
                        PlanoContaId = item.PlanoContaId,
                        PlanoContas = item.PlanoContasDto

                    };

                    registros.Add(HistoricoTransacoesModel);
                });

                return View(registros);
            }
            else if (acao == "Grafico") 
            {
                var dados = new
                {
                    Labels = new[] { "Receitas", "Despesas" },
                    Valores = new[] { totalReceita, totalDespesa }
                };

                return View("GraficoPizza", dados);
            }

            return BadRequest("Ação inválida.");
        }

        [HttpGet]
        public IActionResult RelatorioPorPeriodo()
        {
            return View("Relatorio");
        }

        private static HistoricoTransacoesModel ObterHistoricoTransacoesModel(HistoricoTransacoesDto historicoTransacoesDto)
        {
            return new HistoricoTransacoesModel
            {
                Id = historicoTransacoesDto.Id,
                Descricao = historicoTransacoesDto.Descricao,
                Data = historicoTransacoesDto.Data,
                Valor = historicoTransacoesDto.Valor,
                PlanoContaId = historicoTransacoesDto.PlanoContaId
            };
        }
        private static HistoricoTransacoesDto ObterPlanoContaDto(HistoricoTransacoesModel HistoricoTransacoesModel)
        {
            return new HistoricoTransacoesDto
            {
                Id = HistoricoTransacoesModel.Id,
                Descricao = HistoricoTransacoesModel.Descricao,
                Data = HistoricoTransacoesModel.Data,
                Valor = Convert.ToDecimal(HistoricoTransacoesModel.Valor),
                PlanoContaId = HistoricoTransacoesModel.PlanoContaId
            };
        }
    }
}