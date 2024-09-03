using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCCAlunos2024.Models
{
    [Table("Salas")]
    public class Sala
    {
        public enum TipoSitucao { Liberada, Ocupada, Reservada, Manutencao, Outra}

        [Display(Name ="ID: ")]
        public int id { get; set; }
        
        [Display(Name = "Descrição: ")]
        [StringLength(30)]
        [Required(ErrorMessage ="Campo descrição é obrigatório")]
        public String descricao { get; set; }

        [Display(Name = "Quantidade Equipamentos: ")]
        public int equipamentos { get; set; }

        [Display(Name = "Situação: ")]
        public TipoSitucao situacao { get; set; }
    }
}
