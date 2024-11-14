using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;

namespace myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces
{
    public interface IHistoricoTransacoesServices
    {
        Task<List<HistoricoTransacoesDto>> ObterTodosHistoricoTransacoesAsync();
        Task<HistoricoTransacoesDto> ObterHistoricoTransacaoPorIdAsync(int id);
        Task AdicionarHistoricoTransacoesAsync(HistoricoTransacoesDto HistoricoTransacoessDto);
        Task AtualizarHistoricoTransacoesAsync(HistoricoTransacoesDto HistoricoTransacoess);
        Task RemoverHistoricoTransacoesAsync(HistoricoTransacoesDto HistoricoTransacoessDto);
       
    }
}
