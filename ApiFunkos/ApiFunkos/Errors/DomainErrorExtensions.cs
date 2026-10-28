using Microsoft.AspNetCore.Mvc;

namespace ApiFunkos.Errors;

public static class DomainErrorExtensions {
    public static ActionResult<T> TopHttpResult<T>(this DomainError error) {
        return error switch {
            NotFoundError => new NotFoundObjectResult(new { message = error.Message }),
            ValidationError => new BadRequestObjectResult(new { message = error.Message }),
            _ => new ObjectResult(new { message = "Error interno del servidor" }) { StatusCode = 500 }
        };
    }
}