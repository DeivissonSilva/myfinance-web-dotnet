using myfinance_web_dotnet.App_Code_Clean.Core.DataAcess;
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;
using myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces;

namespace myfinance_web_dotnet.App_Code_Clean.Core.Services
{
    public class HistoricoTransacoesServices : IHistoricoTransacoesServices
    {
        private readonly HistoricoTransacoesRepository _historicoTransacoesRepository;

        public HistoricoTransacoesServices(HistoricoTransacoesRepository historicoTransacoesRepository)
        {
            _historicoTransacoesRepository = historicoTransacoesRepository;
        }

        public async Task<List<HistoricoTransacoesDto>> ObterTodosHistoricoTransacoesAsync()
        {
            var listaTransacoes = await _historicoTransacoesRepository.GetAllWithPlanoContasAsync();
            List<HistoricoTransacoesDto> listaTransacoesDto = [];
            listaTransacoes.ForEach(x => listaTransacoesDto.Add(ObterHistoricoTransacoesDTO(x)));

            return listaTransacoesDto;
        }

        public async Task<HistoricoTransacoesDto> ObterHistoricoTransacaoPorIdAsync(int id)
        {
            var historicoTransacoes = await _historicoTransacoesRepository.GetByIdAsync(id);
            HistoricoTransacoesDto historicoTransacoesDto = ObterHistoricoTransacoesDTO(historicoTransacoes);

            return historicoTransacoesDto;
        }
        public async Task AdicionarHistoricoTransacoesAsync(HistoricoTransacoesDto historicoTransacoesDto)
        {
            var novaTransacao = new HistoricoTransacoes
            {
                Id = historicoTransacoesDto.Id,
                Descricao = historicoTransacoesDto.Descricao,
                PlanoContasId = historicoTransacoesDto.PlanoContaId,
                Data = historicoTransacoesDto.Data,
                Valor = historicoTransacoesDto.Valor
            };

            await _historicoTransacoesRepository.AddAsync(novaTransacao);
        }

        public async Task AtualizarHistoricoTransacoesAsync(HistoricoTransacoesDto historicoTransacoesDto)
        {
            var historicoTransacoesDb = ObterHistoricoTransacoesBD(historicoTransacoesDto);
            await _historicoTransacoesRepository.UpdateAsync(historicoTransacoesDb);
        }

        public async Task RemoverHistoricoTransacoesAsync(HistoricoTransacoesDto historicoTransacoesDto)
        {
            var historicoTransacoesDb = await ObterHistoricoTransacoesPorId(historicoTransacoesDto.Id);
            await _historicoTransacoesRepository.DeleteAsync(historicoTransacoesDb);
        }

        private static HistoricoTransacoesDto ObterHistoricoTransacoesDTO(HistoricoTransacoes historicoTransacoesDb)
        {
            return new HistoricoTransacoesDto
            {
                Id = historicoTransacoesDb.Id,
                Descricao = historicoTransacoesDb.Descricao,
                PlanoContaId = historicoTransacoesDb.PlanoContasId,
                PlanoContasDto = new PlanoContasDto
                {
                    Id = historicoTransacoesDb.PlanoContas.Id,
                    Descricao = historicoTransacoesDb.PlanoContas.Descricao,
                },
                Data = historicoTransacoesDb.Data,
                Valor = historicoTransacoesDb.Valor
            };
        }

        private static HistoricoTransacoes ObterHistoricoTransacoesBD(HistoricoTransacoesDto historicoTransacoesDto)
        {
            return new HistoricoTransacoes
            {
                Id = historicoTransacoesDto.Id,
                Descricao = historicoTransacoesDto.Descricao,
                PlanoContasId = historicoTransacoesDto.PlanoContaId,
                Data = historicoTransacoesDto.Data,
                Valor = historicoTransacoesDto.Valor
            };
        }

        private async  Task<HistoricoTransacoes> ObterHistoricoTransacoesPorId(int id)
        {
            return await _historicoTransacoesRepository.GetByIdAsync(id);
        }
    }
}

