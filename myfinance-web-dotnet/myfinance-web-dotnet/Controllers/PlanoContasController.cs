using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;
using myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces;
using myfinance_web_dotnet.Models;
using System.Diagnostics;

namespace myfinance_web_dotnet.Controllers
{
    public class PlanoContasController : Controller
    {
        private readonly ILogger<PlanoContasController> _logger;
        private readonly IPlanoContasServices _planoContasServices;

        public PlanoContasController(ILogger<PlanoContasController> logger, IPlanoContasServices planoContasServices)
        {
            _logger = logger;
            _planoContasServices = planoContasServices;
        }

        public async Task<IActionResult>  Index()
        {
            //Poderia usar autoMap,mas como é um sistema pequeno e academico fica para futuras implementações.

            var planoContas = await _planoContasServices.ObterTodosPlanosContasAsync();
            List<PlanoContaModel> registros = [];

            foreach (var item in planoContas)
            {
                var planoContaModel = new PlanoContaModel
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Ativo = item.Ativo,
                    Tipo = item.Tipo
                };

                registros.Add(planoContaModel);
            }

            return View(registros);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlanoContaModel planoContaModel)
        {
            if (ModelState.IsValid)
            {
                var novoPlanoContaDto = new PlanoContasDto
                {
                    Descricao = planoContaModel.Descricao,
                    Tipo = planoContaModel.Tipo
                };

                await _planoContasServices.AdicionarPlanoContaAsync(novoPlanoContaDto);

                return RedirectToAction(nameof(Index));
            }

            return View(planoContaModel);   
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoContas = await _planoContasServices.ObterPlanoContaPorIdAsync((int)id);

            if (planoContas == null)
            {
                return NotFound();
            }

            var planoContaModel = ObterPlanoContaModel(planoContas);

            return View(planoContaModel);
        }

        [HttpPost, Route("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var planoContas =  await _planoContasServices.ObterPlanoContaPorIdAsync(Id);

            if (planoContas != null)
            {
                await _planoContasServices.RemoverPlanoContaAsync(planoContas);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static PlanoContaModel ObterPlanoContaModel(PlanoContasDto planoContas)
        {
            return new PlanoContaModel
            {
                Id = planoContas.Id,
                Descricao = planoContas.Descricao,
                Ativo = planoContas.Ativo,
                Tipo = planoContas.Tipo
            };
        }
    }
}