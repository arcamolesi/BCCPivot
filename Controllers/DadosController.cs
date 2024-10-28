using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCCAlunos2024.Models;


namespace BCCAlunos2024.Controllers
{
    public class DadosController : Controller
    {
        private readonly Contexto contexto;

        public DadosController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult Alunos()
        {
            contexto.Database.ExecuteSqlRaw("delete from Alunos");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Alunos', RESEED, 0)");
            Random rand = new Random();

            string[] vNomeMas = { "Miguel", "Arthur", "Bernardo", "Heitor", "Davi", "Lorenzo", "Théo", "Pedro", "Gabriel", "Enzo", "Matheus", "Lucas", "Benjamin", "Nicolas", "Guilherme", "Rafael", "Joaquim", "Samuel", "Enzo Gabriel", "João Miguel", "Henrique", "Gustavo", "Murilo", "Pedro Henrique", "Pietro", "Lucca", "Felipe", "João Pedro", "Isaac", "Benício", "Daniel", "Anthony", "Leonardo", "Davi Lucca", "Bryan", "Eduardo", "João Lucas", "Victor", "João", "Cauã", "Antônio", "Vicente", "Caleb", "Gael", "Bento", "Caio", "Emanuel", "Vinícius", "João Guilherme", "Davi Lucas", "Noah", "João Gabriel", "João Victor", "Luiz Miguel", "Francisco", "Kaique", "Otávio", "Augusto", "Levi", "Yuri", "Enrico", "Thiago", "Ian", "Victor Hugo", "Thomas", "Henry", "Luiz Felipe", "Ryan", "Arthur Miguel", "Davi Luiz", "Nathan", "Pedro Lucas", "Davi Miguel", "Raul", "Pedro Miguel", "Luiz Henrique", "Luan", "Erick", "Martin", "Bruno", "Rodrigo", "Luiz Gustavo", "Arthur Gabriel", "Breno", "Kauê", "Enzo Miguel", "Fernando", "Arthur Henrique", "Luiz Otávio", "Carlos Eduardo", "Tomás", "Lucas Gabriel", "André", "José", "Yago", "Danilo", "Anthony Gabriel", "Ruan", "Miguel Henrique", "Oliver" };
            string[] vNomeFem = { "Alice", "Sophia", "Helena", "Valentina", "Laura", "Isabella", "Manuela", "Júlia", "Heloísa", "Luiza", "Maria Luiza", "Lorena", "Lívia", "Giovanna", "Maria Eduarda", "Beatriz", "Maria Clara", "Cecília", "Eloá", "Lara", "Maria Júlia", "Isadora", "Mariana", "Emanuelly", "Ana Júlia", "Ana Luiza", "Ana Clara", "Melissa", "Yasmin", "Maria Alice", "Isabelly", "Lavínia", "Esther", "Sarah", "Elisa", "Antonella", "Rafaela", "Maria Cecília", "Liz", "Marina", "Nicole", "Maitê", "Isis", "Alícia", "Luna", "Rebeca", "Agatha", "Letícia", "Maria-", "Gabriela", "Ana Laura", "Catarina", "Clara", "Ana Beatriz", "Vitória", "Olívia", "Maria Fernanda", "Emilly", "Maria Valentina", "Milena", "Maria Helena", "Bianca", "Larissa", "Mirella", "Maria Flor", "Allana", "Ana Sophia", "Clarice", "Pietra", "Maria Vitória", "Maya", "Laís", "Ayla", "Ana Lívia", "Eduarda", "Mariah", "Stella", "Ana", "Gabrielly", "Sophie", "Carolina", "Maria Laura", "Maria Heloísa", "Maria Sophia", "Fernanda", "Malu", "Analu", "Amanda", "Aurora", "Maria Isis", "Louise", "Heloise", "Ana Vitória", "Ana Cecília", "Ana Liz", "Joana", "Luana", "Antônia", "Isabel", "Bruna" };

            for (int i = 0; i < 100; i++)
            {
                Aluno aluno = new Aluno();

                aluno.nome = (i % 2 == 0) ? vNomeMas[i / 2] : vNomeFem[i / 2];
                aluno.cursoID = rand.Next(1, 9);
                aluno.periodo = (Aluno.Periodo)rand.Next(1, 3);
                aluno.aniversario = Convert.ToDateTime("01/01/1994").AddDays(rand.Next(0, 4330));

                contexto.Alunos.Add(aluno);
            }

            contexto.SaveChanges();
            return View(contexto.Alunos.ToList());
        }

        public IActionResult Atendimentos()
        {
            contexto.Database.ExecuteSqlRaw("delete from Atendimentos");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Atendimentos', RESEED, 0)");
            Random rand = new Random();
            for (int i = 0; i < 1000; i++)
            { 
                Atendimento atendimento = new Atendimento();
                atendimento.alunoID = rand.Next(1,101);
                atendimento.salaID = rand.Next(1, 7); 
                atendimento.tipo = (rand.Next(1,3) == 1) ? 'A': 'T';
                atendimento.dataHora = Convert.ToDateTime("01/01/2010").AddDays(rand.Next(0, 5407)).AddSeconds(rand.Next(1,86400));
                contexto.Atendimentos.Add(atendimento); 
            }
            contexto.SaveChanges(); 

            return View(contexto.Atendimentos.Include(a => a.aluno).Include(b=>b.sala).ToList());
        }
    }
}
