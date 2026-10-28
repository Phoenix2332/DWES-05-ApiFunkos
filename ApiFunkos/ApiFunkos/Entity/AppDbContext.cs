using ApiFunkos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFunkos.Entity;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<Funko> Funkos => Set<Funko>();
    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Funko>(entity => {
            entity.ToTable("funko");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Nombre)
                .IsRequired()
                .HasPrecision(20);
            entity.Property(f => f.Precio)
                .IsRequired()
                .HasPrecision(20, 2);
            entity.Property(f => f.Stock)
                .IsRequired();
            entity.Property(f => f.CategoriaId)
                .IsRequired();
            entity.HasOne(f => f.Categoria)
                .WithMany(c => c.Funkos)
                .HasForeignKey(f => f.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(f => f.CreatedAt)
                .IsRequired();
            entity.Property(f => f.UpdatedAt)
                .IsRequired(false);
            entity.Property(f => f.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
            entity.HasQueryFilter(f => !f.IsDeleted);
            entity.HasData(
                new Funko {
                    Id = 1, Nombre = "Homer Simpson", Precio = 15.99m, Stock = 10, CategoriaId = 1,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 2, Nombre = "Harry Potter", Precio = 14.50m, Stock = 25, CategoriaId = 2,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 3, Nombre = "Iron Man", Precio = 18.00m, Stock = 12, CategoriaId = 3,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 4, Nombre = "Batman", Precio = 16.50m, Stock = 15, CategoriaId = 4,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 5, Nombre = "Darth Vader", Precio = 17.00m, Stock = 8, CategoriaId = 5,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 6, Nombre = "Goku", Precio = 19.99m, Stock = 20, CategoriaId = 6,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 7, Nombre = "Master Chief", Precio = 16.00m, Stock = 14, CategoriaId = 7,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 8, Nombre = "Mickey Mouse", Precio = 14.00m, Stock = 30, CategoriaId = 8,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 9, Nombre = "Freddie Mercury", Precio = 22.50m, Stock = 5, CategoriaId = 9,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                },
                new Funko {
                    Id = 10, Nombre = "Michael Jordan", Precio = 25.00m, Stock = 7, CategoriaId = 10,
                    CreatedAt = DateTime.UtcNow, IsDeleted = false
                }
            );
        });

        modelBuilder.Entity<Categoria>(entity => {
            entity.ToTable("categorias");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasData(
                new Categoria { Id = 1, Nombre = "Animación" },
                new Categoria { Id = 2, Nombre = "Películas y Series" },
                new Categoria { Id = 3, Nombre = "Marvel" },
                new Categoria { Id = 4, Nombre = "DC Comics" },
                new Categoria { Id = 5, Nombre = "Star Wars" },
                new Categoria { Id = 6, Nombre = "Anime" },
                new Categoria { Id = 7, Nombre = "Videojuegos" },
                new Categoria { Id = 8, Nombre = "Disney" },
                new Categoria { Id = 9, Nombre = "Música" },
                new Categoria { Id = 10, Nombre = "Deportes" }
            );
        });
    }
}