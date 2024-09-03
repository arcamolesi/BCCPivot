using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCCAlunos2024.Models
{
    

    [Table("Alunos")]
    public class Aluno
    {
        public enum Periodo { Manha, Tarde, Noite }

        [Display(Name ="ID: ")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name ="Nome: ")]
        [Required(ErrorMessage = "Campo nome é obrigatório...")]
        [StringLength(35, ErrorMessage = "Tamanho máximo 35 caracteres")]
        public string nome { get; set; }

        [Display(Name = "Data Aniversário: ")]
        [Required(ErrorMessage = "Campo Data Aniversário é obrigatório")]
        public DateTime aniversario { get; set; }

        [Display(Name = "Curso: ")]
        public Curso curso { get; set; }
        [Display(Name = "Curso: ")]
        public int cursoID { get; set; }

        [Display(Name ="Período: ")]
        public Periodo periodo { get; set; }

    }
}
