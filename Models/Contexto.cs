using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BCCAlunos2024.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }



    }
}
