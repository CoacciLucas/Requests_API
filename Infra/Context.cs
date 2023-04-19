using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens);
            modelBuilder.Entity<Item>().Property(e => e.Id).ValueGeneratedNever();
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
