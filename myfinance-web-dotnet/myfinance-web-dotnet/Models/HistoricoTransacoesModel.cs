
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace myfinance_web_dotnet.Models
{
    public class HistoricoTransacoesModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Histórico é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A Data é obrigatória.")]
        [Display(Name = "Data da transação")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O Valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Selecione um Plano de Conta.")]
        public int PlanoContaId { get; set; }
        public PlanoContasDto PlanoContas { get; set; }

    }
}