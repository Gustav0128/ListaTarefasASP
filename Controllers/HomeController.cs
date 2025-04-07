using ListaTarefasProjeto.Data;
using ListaTarefasProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ListaTarefasProjeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(string id)
        {
            var filtros = new Filtros(id);
            
            ViewBag.Filtros = filtros;
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Status = _context.Statuses.ToList();
            ViewBag.ValorVencimento = Filtros.VencimentoValores.ToList();

            IQueryable<Tarefas> consulta = _context.Tarefas.
                                            Include(c => c.Categoria).
                                            Include(s => s.Status);

            if (filtros.TemCategoria)
            {
                consulta = consulta.Where(t => t.CategoriaId == filtros.CategoriaId);
            }
            if (filtros.TemStatus)
            {
                consulta = consulta.Where(t => t.StatusId == filtros.StatusId);
            }
            if (filtros.TemVencimento)
            {
                var hoje = DateTime.Today;

                if (filtros.EPassado)
                {
                    consulta = consulta.Where(t => t.DataDeVencimento < hoje);
                }
                if (filtros.EFuturo)
                {
                    consulta = consulta.Where(t => t.DataDeVencimento > hoje);
                }
                if (filtros.EHoje)
                {
                    consulta = consulta.Where(t => t.DataDeVencimento == hoje);
                }
            }


            var tarefas = consulta.OrderBy(t => t.DataDeVencimento).ToList();


            return View(tarefas);
        }

        [HttpPost]
        public IActionResult Filtrar(string[] filtro)
        {
            string id = string.Join('-', filtro);
            return RedirectToAction ("Index", new {ID = id});
        }

        public IActionResult MarcarCompleto([FromRoute] string id, Tarefas tarefaSelecionada)
        {
            tarefaSelecionada = _context.Tarefas.Find(tarefaSelecionada.Id);
            if(tarefaSelecionada.Id != null)
            {
                tarefaSelecionada.StatusId = "completo";
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new {ID = id});
        }


    }
}
