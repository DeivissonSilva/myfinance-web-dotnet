
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;

namespace myfinance_web_dotnet.Models
{
    public class HistoricoTransacoesModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }
        public int PlanoContaId { get; set; }
        public PlanoContasDto PlanoContas { get; set; }

    }
}