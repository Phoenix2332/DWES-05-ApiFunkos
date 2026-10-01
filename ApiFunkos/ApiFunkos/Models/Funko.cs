using ApiFunkos.Enums;

namespace ApiFunkos.Models;

public record Funko {
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public double Precio { get; init; }
    public int Stock { get; init; }
    public Categoria Categoria { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; init; }
    public bool IsActivo => DeletedAt is null;
}