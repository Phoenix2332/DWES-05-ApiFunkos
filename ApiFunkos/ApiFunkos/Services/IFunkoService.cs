using ApiFunkos.Dto.Api;
using ApiFunkos.Dto.Funkos;
using ApiFunkos.Errors;
using CSharpFunctionalExtensions;

namespace ApiFunkos.Services;

public interface IFunkoService {
    Task<IReadOnlyList<FunkoDto>> GetAll();
    Task<IReadOnlyCollection<FunkoDto>> GetDeleted();
    Task<Result<FunkoDto, DomainError>> GetById(int id);
    Task<Result<FunkoDto, DomainError>> Add(CreateFunkoDto funko);
    Task<Result<FunkoDto, DomainError>> Update(int id, UpdateFunkoDto funko);
    Task<Result<bool, DomainError>> SoftDelete(int id);
    Task<Result<bool, DomainError>> Restore(int id);
    Task<IReadOnlyList<CategoriaDto>> GetAllCategories();
    Task<Result<CategoriaDto, DomainError>> GetByIdCategories(int id);
}