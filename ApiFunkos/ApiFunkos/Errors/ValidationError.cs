namespace ApiFunkos.Errors;

public sealed record ValidationError(string Message) : DomainError(Message);