using ApiFunkos.Errors;
using ApiFunkos.Models;
using CSharpFunctionalExtensions;

namespace ApiFunkos.Repositories.Funkos;

public interface IFunkoRepository {
    Task<IReadOnlyList<Funko>> GetAll();
    Task<Result<Funko, DomainError>> GetById(int id);
    Task<IReadOnlyList<Funko>> GetAllDeleted();
    Task<Result<Funko, DomainError>> Add(Funko funko);
    Task<Result<Funko, DomainError>> Update(int id, Funko funko);
    Task<Result<bool, DomainError>> SoftDelete(int id);
    Task<Result<bool, DomainError>> Restore(int id);
}