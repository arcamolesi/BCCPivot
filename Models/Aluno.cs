using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCCAlunos2024.Models
{
    public enum Periodo {Manha, Tarde, Noite}

    [Table("Alunos")]
    public class Aluno
    {
        [Display(Name ="ID: ")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name ="Nome: ")]
        [Required(ErrorMessage = "Campo nome é obrigatório...")]
        [StringLength(35, ErrorMessage = "Tamanho máximo 35 caracteres")]
        public string nome { get; set; }

        [Display(Name = "Data Aniversário: ")]
        public DateTime aniversario { get; set; }

        [Display(Name = "Curso: ")]
        public Curso curso { get; set; }
        public int cursoID { get; set; }

        [Display(Name ="Período: ")]
        public Periodo periodo { get; set; }

    }
}
