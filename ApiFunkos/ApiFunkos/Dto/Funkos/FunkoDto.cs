namespace ApiFunkos.Dto.Funkos;

public record FunkoDto {
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public decimal Precio { get; init; }
    public int Stock { get; init; }
    public string Categoria { get; init; } = string.Empty;
    public int CategoriaId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}