namespace ApiFunkos.Errors;

public sealed record NotFoundError(string Message) : DomainError(Message) {
    public static NotFoundError FromId(int id, string resource = "Recurso") {
        return new NotFoundError($"{resource} con ID {id} no encontrado");
    }
}