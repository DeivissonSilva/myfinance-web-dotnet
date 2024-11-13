using myfinance_web_dotnet.App_Code_Clean.Core.DataAcess;
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;
using myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces;

namespace myfinance_web_dotnet.App_Code_Clean.Core.Services
{
    public class PlanoContasServices : IPlanoContasServices
    {
        private readonly PlanoContasRepository _planoContasRepository;

        public PlanoContasServices(PlanoContasRepository planoContasRepository)
        {
            _planoContasRepository = planoContasRepository;
        }

        public async Task<List<PlanoContas>> ObterTodosPlanosContasAsync()
        {

            return await _planoContasRepository.GetAllAsync();
        }

        public async Task<PlanoContasDto> ObterPlanoContaPorIdAsync(int id)
        {
            var planoContas = await _planoContasRepository.GetByIdAsync(id);
            PlanoContasDto PlanoContasDto = ObterPlanoContaDTO(planoContas);

            return PlanoContasDto;
        }

        //public async Task<List<PlanoContas>> BuscarPorNomeAsync(string nome)
        //{
        //    return await _planoContasRepository.BuscarPorNomeAsync(nome);
        //}

        public async Task AdicionarPlanoContaAsync(PlanoContasDto planoContasDto)
        {
            var novoPlanoContaEntidade = new PlanoContas
            {
                Id = 0,
                Ativo = true,
                Descricao = planoContasDto.Descricao,
                Tipo = planoContasDto.Tipo
            };

            await _planoContasRepository.AddAsync(novoPlanoContaEntidade);
        }

        //public async Task AtualizarPlanoContaAsync(PlanoContas planoContas)
        //{
        //    await _planoContasRepository.UpdateAsync(planoContas);
        //}

        public async Task RemoverPlanoContaAsync(PlanoContasDto planoContasDto)
        {
            var planoContasBD = await ObterPlanoContaPorId(planoContasDto.Id);
            await _planoContasRepository.DeleteAsync(planoContasBD);
        }

        private static PlanoContasDto ObterPlanoContaDTO(PlanoContas planoContas)
        {
            return new PlanoContasDto
            {
                Id = planoContas.Id,
                Descricao = planoContas.Descricao,
                Ativo = planoContas.Ativo,
                Tipo = planoContas.Tipo
            };
        }

        private static PlanoContas ObterPlanoContaBD(PlanoContasDto planoContasDto)
        {
            return new PlanoContas
            {
                Id = planoContasDto.Id,
                Descricao = planoContasDto.Descricao,
                Ativo = planoContasDto.Ativo,
                Tipo = planoContasDto.Tipo
            };
        }

        private async  Task<PlanoContas> ObterPlanoContaPorId(int id)
        {
            return await _planoContasRepository.GetByIdAsync(id);
        }
    }
}

