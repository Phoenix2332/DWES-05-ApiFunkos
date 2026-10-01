using ApiFunkos.Enums;
using ApiFunkos.Models;

namespace ApiFunkos.Repositories;

public class FunkoRepository : IFunkoRepository {
    private readonly Dictionary<int, Funko> _funkos = [];
    private int _nextId = 1;

    public Task<IReadOnlyList<Funko>> GetAllAsync() {
        return Task.FromResult<IReadOnlyList<Funko>>(Activos());
    }

    public Task<Funko?> GetByIdAsync(int id) {
        return Task.FromResult(TryGet(id));
    }

    public Task<IReadOnlyList<Funko>> GetByNombreAsync(string nombre) {
        var texto = nombre ?? string.Empty;
        var resultado = Activos()
            .Where(f => f.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f.Nombre)
            .ToList();
        return Task.FromResult<IReadOnlyList<Funko>>(resultado);
    }

    public Task<IReadOnlyList<Funko>> GetByCategoriaAsync(Categoria categoria) {
        var resultado = Activos()
            .Where(f => f.Categoria == categoria)
            .ToList();
        return Task.FromResult<IReadOnlyList<Funko>>(resultado);
    }

    public Task<Funko> AddAsync(Funko funko) {
        var nuevo = funko with {
            Id = _nextId++,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _funkos[nuevo.Id] = nuevo;
        return Task.FromResult(nuevo);
    }

    public Task<Funko?> UpdateAsync(int id, Funko funko) {
        if (TryGet(id) is not { } existente)
            return Task.FromResult<Funko?>(null);
        var actualizado = existente with {
            Nombre = funko.Nombre,
            Precio = funko.Precio,
            Stock = funko.Stock,
            Categoria = funko.Categoria,
            UpdatedAt = DateTime.UtcNow
        };
        _funkos[id] = actualizado;
        return Task.FromResult<Funko?>(actualizado);
    }

    public Task<bool> SoftDeleteAsync(int id) {
        if (TryGet(id) is not { } existente)
            return Task.FromResult(false);
        _funkos[id] = existente with {
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = DateTime.UtcNow
        };
        return Task.FromResult(true);
    }

    private List<Funko> Activos() {
        return [.. _funkos.Values.Where(p => p.IsActivo)];
    }

    private Funko? TryGet(int id) {
        return _funkos.TryGetValue(id, out var producto) && producto.IsActivo ? producto : null;
    }
}