using ApiFunkos.Errors;
using ApiFunkos.Models;
using CSharpFunctionalExtensions;

namespace ApiFunkos.Repositories.Categorias;

public interface ICategoriaRepository {
    Task<IReadOnlyList<Categoria>> GetAll();
    Task<Result<Categoria, DomainError>> GetById(int id);
}