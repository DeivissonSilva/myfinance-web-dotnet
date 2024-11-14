using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;

namespace myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces
{
    public interface IPlanoContasServices
    {
        Task<List<PlanoContasDto>> ObterTodosPlanosContasAsync();
        Task<PlanoContasDto> ObterPlanoContaPorIdAsync(int id);
        Task AdicionarPlanoContaAsync(PlanoContasDto planoContasDto);
        Task RemoverPlanoContaAsync(PlanoContasDto planoContasDto);
        Task AtualizarPlanoContaAsync(PlanoContasDto planoContas);
    }
}
