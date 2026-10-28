using ApiFunkos.Entity;
using ApiFunkos.Errors;
using ApiFunkos.Infrastructure.Interfaces;
using ApiFunkos.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApiFunkos.Repositories.Categorias;

public class CategoriaRespository(AppDbContext context) : ICategoriaRepository, IScopedService {
    public async Task<IReadOnlyList<Categoria>> GetAll() {
        return await context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<Result<Categoria, DomainError>> GetById(int id) {
        var categoria = await context.Categorias.FindAsync(id);
        return categoria is not null
            ? Result.Success<Categoria, DomainError>(categoria)
            : Result.Failure<Categoria, DomainError>(NotFoundError.FromId(id, "Funko"));
    }
}