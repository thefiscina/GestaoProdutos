using Microsoft.EntityFrameworkCore;

namespace GestaoProduto.Dominio
{
    public sealed class GestaoProdutoContext : DbContext
    {
        public GestaoProdutoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");        
            modelBuilder.Entity<Product>().ToTable("product");
        }
    }
}