namespace ApiFunkos.Errors;

public abstract record DomainError(string Message) {
    public override string ToString() {
        return $"{GetType().Name}: {Message}";
    }
}