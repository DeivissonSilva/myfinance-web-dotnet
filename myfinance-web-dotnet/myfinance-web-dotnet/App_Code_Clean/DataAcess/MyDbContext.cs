using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;

namespace myfinance_web_dotnet.App_Code_Clean.Core.DataAcess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<PlanoContas> PlanoContas { get; set; }
        public DbSet<HistoricoTransacoes> HistoricoTransacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-UQJ98U7;Initial Catalog=myfinance;User Id=sa;Password=SL@sp01$;");
            //}
        }
    }
}
