using ApiFunkos.Dto.Api;
using ApiFunkos.Dto.Funkos;
using ApiFunkos.Models;

namespace ApiFunkos.Mappers;

public static class FunkoMapper {
    public static FunkoDto ToDto(this Funko f) {
        return new FunkoDto {
            Id = f.Id,
            Nombre = f.Nombre,
            Precio = f.Precio,
            Stock = f.Stock,
            CategoriaId = f.CategoriaId,
            Categoria = f.Categoria?.Nombre ?? string.Empty,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt
        };
    }

    public static Funko ToModel(this CreateFunkoDto dto) {
        return new Funko {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            Stock = dto.Stock,
            CategoriaId = dto.CategoriaId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            IsDeleted = false
        };
    }

    public static Funko ToModel(this UpdateFunkoDto dto) {
        return new Funko {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            Stock = dto.Stock,
            CategoriaId = dto.CategoriaId,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static CategoriaDto ToDto(this Categoria c) {
        return new CategoriaDto(
            c.Id,
            c.Nombre
        );
    }
}