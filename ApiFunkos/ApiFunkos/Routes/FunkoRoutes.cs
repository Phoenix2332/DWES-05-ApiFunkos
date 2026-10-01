using ApiFunkos.Enums;
using ApiFunkos.Models;
using ApiFunkos.Repositories;

namespace ApiFunkos.Routes;

public static class FunkoRoutes {
    public static void MapFunkoRoutes(this WebApplication app) {
        var repository = app.Services.GetRequiredService<IFunkoRepository>();

        var group = app.MapGroup("/api/funkos").WithTags("Funkos");

        group.MapGet("/", async () => Results.Ok(await repository.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id) => await repository.GetByIdAsync(id) is { } funko
            ? Results.Ok(funko)
            : Results.NotFound());

        group.MapPost("/", async (Funko funko) => {
            var creado = await repository.AddAsync(funko);
            return Results.Created($"/api/funkos/{creado.Id}", creado);
        });

        group.MapPut("/{id:int}", async (int id, Funko funko) =>
            await repository.UpdateAsync(id, funko) is { } actualizado
                ? Results.Ok(actualizado)
                : Results.NotFound());

        group.MapDelete("/{id:int}", async (int id) => await repository.SoftDeleteAsync(id)
            ? Results.NoContent()
            : Results.NotFound());

        group.MapGet("/search", async (string nombre) => Results.Ok(await repository.GetByNombreAsync(nombre)));

        group.MapGet("/categoria/{categoria}",
            async (Categoria categoria) => Results.Ok(await repository.GetByCategoriaAsync(categoria)));
    }
}