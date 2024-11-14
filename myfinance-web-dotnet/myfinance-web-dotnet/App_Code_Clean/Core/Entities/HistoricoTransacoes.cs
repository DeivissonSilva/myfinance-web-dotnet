namespace myfinance_web_dotnet.App_Code_Clean.Core.Entities
{
    public class HistoricoTransacoes
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }
        public int PlanoContasId { get; set; }
        public PlanoContas PlanoContas { get; set; }
    }
}
