using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using BCCAlunos2024.Models.Consulta;

namespace BCCAlunos2024.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<BCCAlunos2024.Models.Consulta.AtendimentoGrp> AtendimentoGrp { get; set; }
        public DbSet<BCCAlunos2024.Models.Consulta.AtendimentoAnoMes> AtendimentoAnoMes { get; set; }
        public DbSet<BCCAlunos2024.Models.Consulta.PivotAtendimento> PivotAtendimento { get; set; }

    }
}
