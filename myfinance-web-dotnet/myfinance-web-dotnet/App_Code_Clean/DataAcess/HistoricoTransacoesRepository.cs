using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;

namespace myfinance_web_dotnet.App_Code_Clean.Core.DataAcess
{
    public class HistoricoTransacoesRepository : Repository<HistoricoTransacoes>
    {
        public HistoricoTransacoesRepository(MyDbContext context) : base(context) { }

        //Futuros metodos especificos

        public async Task<List<HistoricoTransacoes>> GetAllWithPlanoContasAsync()
        {
            return await _dbSet
                        .Include(c => c.PlanoContas).ToListAsync();
        }

        public async Task<List<HistoricoTransacoes>> ObterTransacoesPorPeriodoDb(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet.Where(t => t.Data >= dataInicio && t.Data <= dataFim)
                .Include(c => c.PlanoContas)
                .OrderByDescending(item=> item.PlanoContas.Tipo).ToListAsync();
        }
    }
}
