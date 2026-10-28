using ApiFunkos.Dto.Api;
using ApiFunkos.Dto.Funkos;
using ApiFunkos.Errors;
using ApiFunkos.Infrastructure.Interfaces;
using ApiFunkos.Mappers;
using ApiFunkos.Repositories.Categorias;
using ApiFunkos.Repositories.Funkos;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace ApiFunkos.Services;

public class FunkoService(
    IFunkoRepository repo,
    ICategoriaRepository cateRepo,
    ILogger<FunkoService> log,
    IValidator<CreateFunkoDto> createValidation,
    IValidator<UpdateFunkoDto> updateValidation
) : IFunkoService, IScopedService {
    public async Task<IReadOnlyList<FunkoDto>> GetAll() {
        log.LogInformation("Getting all funkos");
        var funkos = await repo.GetAll();
        return funkos.Select(t => t.ToDto()).ToList();
    }

    public async Task<IReadOnlyCollection<FunkoDto>> GetDeleted() {
        log.LogInformation("Getting deleted funkos");
        var deleted = await repo.GetAllDeleted();
        return deleted.Select(t => t.ToDto()).ToList();
    }

    public async Task<Result<FunkoDto, DomainError>> GetById(int id) {
        log.LogInformation("Getting funko by id: {@Id}", id);
        var funko = await repo.GetById(id);
        return funko.Map(p => p.ToDto());
    }

    public async Task<Result<FunkoDto, DomainError>> Add(CreateFunkoDto funko) {
        log.LogInformation("Adding funko: {@Funko}", funko);
        var validation = await createValidation.ValidateAsync(funko);
        if (!validation.IsValid) {
            log.LogWarning("Validación fallida al crear funko: {@Errors}", validation.Errors);
            return Result.Failure<FunkoDto, DomainError>(
                new ValidationError(validation.Errors[0].ErrorMessage));
        }

        var creado = await repo.Add(funko.ToModel());
        return creado.Map(p => p.ToDto());
    }

    public async Task<Result<FunkoDto, DomainError>> Update(int id, UpdateFunkoDto funko) {
        log.LogInformation("Updating funko {Id} with: {@Funko}", id, funko);

        var validation = await updateValidation.ValidateAsync(funko);
        if (!validation.IsValid) {
            log.LogWarning("Validación fallida al actualizar funko {Id}: {@Errors}", id, validation.Errors);
            return Result.Failure<FunkoDto, DomainError>(
                new ValidationError(validation.Errors[0].ErrorMessage));
        }

        log.LogInformation("Realizando actualizacion del funko con id: {@Id}", id);
        return (await repo.Update(id, funko.ToModel())).Map(p => p.ToDto());
    }

    public async Task<Result<bool, DomainError>> SoftDelete(int id) {
        log.LogInformation("Soft deleting funko: {@Id}", id);
        return await repo.SoftDelete(id);
    }

    public async Task<Result<bool, DomainError>> Restore(int id) {
        log.LogInformation("Restoring funko: {@Id}", id);
        return await repo.Restore(id);
    }

    public async Task<IReadOnlyList<CategoriaDto>> GetAllCategories() {
        log.LogInformation("Getting all categories");
        return (await cateRepo.GetAll()).Select(p => p.ToDto()).ToList();
    }

    public async Task<Result<CategoriaDto, DomainError>> GetByIdCategories(int id) {
        log.LogInformation("Getting category by id: {@Id}", id);
        return (await cateRepo.GetById(id)).Map(p => p.ToDto());
    }
}