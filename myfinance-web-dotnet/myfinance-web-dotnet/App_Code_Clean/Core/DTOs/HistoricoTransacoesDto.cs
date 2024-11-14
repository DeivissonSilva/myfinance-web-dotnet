using myfinance_web_dotnet.App_Code_Clean.Core.Entities;

namespace myfinance_web_dotnet.App_Code_Clean.Core.DTOs
{
    public class HistoricoTransacoesDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }
        public int PlanoContaId { get; set; }
        public PlanoContasDto PlanoContasDto { get; set; }
    }
}
