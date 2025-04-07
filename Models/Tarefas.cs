using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ListaTarefasProjeto.Models
{
    public class Tarefas
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe a Data")]
        public DateTime? DataDeVencimento { get; set; }
        [Required(ErrorMessage = "Informe uma Categoria")]
        public string CategoriaId { get; set; }
        [ValidateNever]
        public Categoria Categoria { get; set; }
        [Required(ErrorMessage = "Informe um Status")]
        public string StatusId { get; set; }
        [ValidateNever]
        public Status Status { get; set; }
        public bool Atrasado => StatusId == "aberto" && DataDeVencimento < DateTime.Today;
    }
}
