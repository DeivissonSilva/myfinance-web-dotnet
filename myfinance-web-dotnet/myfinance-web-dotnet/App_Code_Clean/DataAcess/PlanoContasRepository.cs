using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;

namespace myfinance_web_dotnet.App_Code_Clean.Core.DataAcess
{
    public class PlanoContasRepository : Repository<PlanoContas>
    {
        public PlanoContasRepository(MyDbContext context) : base(context) { }

        // Futuros metodos especificos
        // Exemplo abaixo
        public async Task<List<PlanoContas>> BuscarPorNomeAsync(string nome)
        {
            return await _dbSet
                .Where(c => c.Descricao.Contains(nome))
                .ToListAsync();
        }
    }
}
