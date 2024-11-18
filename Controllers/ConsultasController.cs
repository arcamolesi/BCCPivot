using BCCAlunos2024.Models;
using BCCAlunos2024.Models.Consulta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using BCCAlunos2024.Extra; 

namespace BCCAlunos2024.Controllers
{
    public class ConsultasController : Controller
    {

        private readonly Contexto contexto;

        public ConsultasController(Contexto context)
        {
            contexto = context;
        }


        public IActionResult AtendimentosPorTipo()
        {
            var lista = contexto.Atendimentos
                .Include(a => a.aluno).Include(s => s.sala)
                .OrderBy(t => t.tipo).ThenBy(s => s.sala.descricao)
                .ThenByDescending(a => a.aluno.nome)
                .ToList();

            return View(lista);
        }


        public IActionResult Alunos()
        {
            var alunos = contexto.Alunos.Include(c => c.curso)
                .OrderBy(o => o.curso.descricao)
                .ThenByDescending(p => p.periodo)
                .ThenBy(o1 => o1.aniversario)
                .ToList();
            return View(alunos);
        }

        public IActionResult FiltrarAluno()
        {
            return View();
        }

        public IActionResult ResFiltrarAluno(int? id, string nome, DateTime? dataIni, DateTime? dataFim)
        {
            List<Aluno> listaAlunos = new List<Aluno>();

            if (dataIni != null && dataFim != null) //filtra por período de data
                listaAlunos = contexto.Alunos.Include(a => a.curso).Where(dt => dt.aniversario >= dataIni && dt.aniversario <= dataFim).OrderBy(n => n.aniversario).ToList();

            else if (id != null) //filtrar por id
                listaAlunos = contexto.Alunos.Include(a => a.curso).Where(a => a.id == id).ToList();
            else
                   if (!nome.IsNullOrEmpty()) //filtrar por nome 
                                              //listaAlunos = contexto.Alunos.Include(a => a.curso).Where(n=>n.nome==nome).ToList();
                listaAlunos = contexto.Alunos.Include(a => a.curso).Where(n => n.nome.Contains(nome)).OrderBy(o => o.nome).ToList();

            else
                listaAlunos = contexto.Alunos.Include(a => a.curso).ToList();

            return View(listaAlunos);
        }

        public IActionResult AgruparAtendimentoCursoSala()
        {
            IEnumerable<AtendimentoGrp> lstGrupoAtendimento = from item in contexto.Atendimentos
                                   .Include(a => a.aluno).Include(c => c.aluno.curso).Include(s => s.sala)
                                   .ToList()
                                                              let curso = item.aluno.curso.descricao
                                                              let sala = item.sala.descricao

                                                              group item by new { curso, sala }
                                   into grupo
                                                              orderby grupo.Key.curso, grupo.Key.sala
                                                              select new AtendimentoGrp
                                                              {
                                                                  curso = grupo.Key.curso,
                                                                  sala = grupo.Key.sala,
                                                                  valor = grupo.Count()


                                                              };

            return View(lstGrupoAtendimento);

        }


        public IActionResult AgruparAtendimentoAnoMes()
        {
            IEnumerable<AtendimentoAnoMes> lstAtendAnoMes  = from item in contexto.Atendimentos
                                    .ToList()
                                                              let ano = item.dataHora.Year
                                                              let mes = item.dataHora.Month

                                                              group item by new { ano, mes }
                                   into grupo
                                                              orderby grupo.Key.ano, grupo.Key.mes
                                                              select new AtendimentoAnoMes
                                                              {
                                                                  ano = grupo.Key.ano,
                                                                  mes = grupo.Key.mes,
                                                                  quantidade = grupo.Count()
                                                              };

            return View(lstAtendAnoMes);

        }


        public IActionResult Pivot()
        {

            IEnumerable<AtendimentoAnoMes> lstAtendAnoMes = from item in contexto.Atendimentos
                                    .ToList()
                                                            let ano = item.dataHora.Year
                                                            let mes = item.dataHora.Month
                                                            group item by new { ano, mes }
                                   into grupo
                                                            orderby grupo.Key.ano, grupo.Key.mes
                                                            select new AtendimentoAnoMes
                                                            {
                                                                ano = grupo.Key.ano,
                                                                mes = grupo.Key.mes,
                                                                quantidade = grupo.Count()
                                                            };

            //Gerar Pivot
            var PivotTableAnoMes = lstAtendAnoMes.ToList().ToPivotTable(
                    pivo => pivo.mes, //coluna
                    pivo => pivo.ano, //linha
                    pivos => (pivos.Any() ? pivos.Sum(x => Convert.ToSingle(x.quantidade)) : 0)); //valor das células

            //Converter DataTable do Pivot para Lista, permitir que o asp net core, imprima depois
            List<PivotAtendimento> lista = new List<PivotAtendimento>();
            lista = (from DataRow linha in PivotTableInsArea.Rows
                     select new PivotAtendimento()
                     {
                         ano = linha[0].ToString(),
                         janeiro = Convert.ToSingle(linha[1]),
                         fevereiro = Convert.ToSingle(linha[2]),
                         marco = Convert.ToSingle(linha[3]),
                         abril = Convert.ToSingle(linha[4]),
                         maio = Convert.ToSingle(linha[5]),
                         junho = Convert.ToSingle(linha[6]),
                         julho = Convert.ToSingle(linha[7]),
                         agosto = Convert.ToSingle(linha[8]),
                         setembro = Convert.ToSingle(linha[9]),
                         outubro = Convert.ToSingle(linha[10]),
                         novembro = Convert.ToSingle(linha[11]),
                         dezembro = Convert.ToSingle(linha[12]),
                         total = Convert.ToSingle(linha[1]) + Convert.ToSingle(linha[2]) + Convert.ToSingle(linha[3]) + Convert.ToSingle(linha[4]) + Convert.ToSingle(linha[5]) + Convert.ToSingle(linha[6]) +
                                 Convert.ToSingle(linha[7]) + Convert.ToSingle(linha[8]) + Convert.ToSingle(linha[9]) + Convert.ToSingle(linha[10]) + Convert.ToSingle(linha[11]) + Convert.ToSingle(linha[12]),
                         }).ToList();

            return View(lista);
        }


    }
}
