using ApiFunkos.Enums;
using ApiFunkos.Models;
using ApiFunkos.Repositories;

namespace ApiFunkos.Factories;

public static class FunkoFactory {
    public static async Task Seed(IFunkoRepository repository) {
        var funkos = new List<Funko> {
            new() { Nombre = "Spider-Man", Precio = 15.99, Stock = 10, Categoria = Categoria.Comics },
            new() { Nombre = "Batman", Precio = 17.99, Stock = 8, Categoria = Categoria.Comics },
            new() { Nombre = "Iron Man", Precio = 18.99, Stock = 12, Categoria = Categoria.Peliculas },
            new() { Nombre = "Darth Vader", Precio = 21.99, Stock = 6, Categoria = Categoria.CienciaFiccion },
            new() { Nombre = "Goku", Precio = 16.99, Stock = 15, Categoria = Categoria.Animacion },
            new() { Nombre = "Naruto", Precio = 14.99, Stock = 20, Categoria = Categoria.Animacion },
            new() { Nombre = "Geralt de Rivia", Precio = 19.99, Stock = 7, Categoria = Categoria.Videojuegos },
            new() { Nombre = "Kratos", Precio = 22.99, Stock = 5, Categoria = Categoria.Videojuegos },
            new() { Nombre = "Harry Potter", Precio = 16.99, Stock = 11, Categoria = Categoria.Fantasia },
            new() { Nombre = "Pennywise", Precio = 18.99, Stock = 4, Categoria = Categoria.Terror }
        };
        foreach (var funko in funkos)
            await repository.AddAsync(funko);
    }
}