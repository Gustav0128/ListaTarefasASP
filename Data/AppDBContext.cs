using ListaTarefasProjeto.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefasProjeto.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Tarefas> Tarefas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                    new Categoria { CategoriaId= "trabalho", Nome= "Trabalho"},
                    new Categoria { CategoriaId = "casa", Nome = "Casa" },
                    new Categoria { CategoriaId = "faculdade", Nome = "Faculdade" },
                    new Categoria { CategoriaId = "compras", Nome = "Compras" },
                    new Categoria { CategoriaId = "academia", Nome = "Academia" }
                );

            modelBuilder.Entity<Status>().HasData(
                    new Status { StatusId= "aberto", Nome= "Aberto"},
                    new Status { StatusId= "completo", Nome= "Completo"}
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
