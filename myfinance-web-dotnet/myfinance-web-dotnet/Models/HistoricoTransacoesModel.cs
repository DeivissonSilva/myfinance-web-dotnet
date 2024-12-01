
using myfinance_web_dotnet.App_Code_Clean.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace myfinance_web_dotnet.Models
{
    public class HistoricoTransacoesModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Hist�rico � obrigat�rio.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A Data � obrigat�ria.")]
        [Display(Name = "Data da transa��o")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O Valor � obrigat�rio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Selecione um Plano de Conta.")]
        public int PlanoContaId { get; set; }
        public PlanoContasDto PlanoContas { get; set; }

    }
}