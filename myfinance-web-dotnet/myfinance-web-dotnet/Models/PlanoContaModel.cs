using System.ComponentModel.DataAnnotations;

namespace myfinance_web_dotnet.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome � obrigat�rio.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Voc� deve selecionar Receita ou Despesa.")]
        public string Tipo { get; set; }
        public bool Ativo { get; set; }
    }
}