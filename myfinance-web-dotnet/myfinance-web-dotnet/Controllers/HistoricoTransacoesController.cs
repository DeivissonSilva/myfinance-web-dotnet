using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;
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
            //Poderia usar autoMap,mas como � um sistema pequeno e academico fica para futuras implementa��es.

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

            var planoContas = await _planoContasServices.ObterTodosPlanosContasAsync();
            planoContas = planoContas.OrderBy(item => item.Descricao).ToList();
            ViewBag.PlanoContaList = new SelectList(planoContas, "Id", "Descricao");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HistoricoTransacoesModel HistoricoTransacoesModel)
        {
            if (ModelState.IsValid)
            {
                var novaTransacao = new HistoricoTransacoesDto
                {
                    Descricao = HistoricoTransacoesModel.Descricao,
                    Data = HistoricoTransacoesModel.Data,
                    Valor = (HistoricoTransacoesModel.Valor / 100),
                    PlanoContaId = HistoricoTransacoesModel.PlanoContaId,
                };

                await _historicoTransacoesServices.AdicionarHistoricoTransacoesAsync(novaTransacao);

                return RedirectToAction(nameof(Index));
            }

            return View(HistoricoTransacoesModel);
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

            var HistoricoTransacoesModel = ObterHistoricoTransacoesModel(historicoTransacao);

            return View(HistoricoTransacoesModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HistoricoTransacoesModel HistoricoTransacoesModel)
        {
            if (id != HistoricoTransacoesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var historicoTransacoesModel = ObterPlanoContaDto(HistoricoTransacoesModel);
                await _historicoTransacoesServices.AtualizarHistoricoTransacoesAsync(historicoTransacoesModel);

                return RedirectToAction(nameof(Index));
            }

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

        [HttpPost, Route("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
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

        private static HistoricoTransacoesModel ObterHistoricoTransacoesModel(HistoricoTransacoesDto historicoTransacoesDto)
        {
            return new HistoricoTransacoesModel
            {
                Id = historicoTransacoesDto.Id,
                Descricao = historicoTransacoesDto.Descricao,
                Data = historicoTransacoesDto.Data,
                //Valor = historicoTransacoesDto.Valor.ToString(),
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