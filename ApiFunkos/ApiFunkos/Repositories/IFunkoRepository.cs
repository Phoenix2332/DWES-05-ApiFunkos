using ApiFunkos.Enums;
using ApiFunkos.Models;

namespace ApiFunkos.Repositories;

public interface IFunkoRepository {
    Task<IReadOnlyList<Funko>> GetAllAsync();

    Task<Funko?> GetByIdAsync(int id);

    Task<IReadOnlyList<Funko>> GetByNombreAsync(string nombre);

    Task<IReadOnlyList<Funko>> GetByCategoriaAsync(Categoria categoria);

    Task<Funko> AddAsync(Funko funko);

    Task<Funko?> UpdateAsync(int id, Funko funko);

    Task<bool> SoftDeleteAsync(int id);
}