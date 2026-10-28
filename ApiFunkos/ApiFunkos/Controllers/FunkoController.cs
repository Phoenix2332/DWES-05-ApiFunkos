using ApiFunkos.Dto.Api;
using ApiFunkos.Dto.Funkos;
using ApiFunkos.Errors;
using ApiFunkos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFunkos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FunkoController(IFunkoService service) : ControllerBase {
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FunkoDto>>> GetAll() {
        var funkos = await service.GetAll();
        return Ok(funkos.ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FunkoDto>> GetById(int id) {
        var resultado = await service.GetById(id);
        return resultado.IsSuccess
            ? Ok(resultado.Value)
            : resultado.Error.TopHttpResult<FunkoDto>();
    }

    [HttpGet("deleted")]
    public async Task<ActionResult<FunkoDto>> GetDeleted() {
        var get = await service.GetDeleted();
        return Ok(get.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<FunkoDto>> Create([FromBody] CreateFunkoDto funko) {
        var result = await service.Add(funko);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : result.Error.TopHttpResult<FunkoDto>();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<FunkoDto>> Update([FromBody] UpdateFunkoDto funko, int id) {
        var act = await service.Update(id, funko);
        return act.IsSuccess
            ? Ok(act.Value)
            : act.Error.TopHttpResult<FunkoDto>();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id) {
        var result = await service.SoftDelete(id);
        return result.IsSuccess
            ? NoContent()
            : result.Error.TopHttpResult<bool>();
    }

    [HttpPost("{id:int}/restore")]
    public async Task<ActionResult<bool>> Restore(int id) {
        var result = await service.Restore(id);
        if (result.IsSuccess) return Ok(result.Value);
        var err = result.Error;
        return result.Error switch {
            NotFoundError notFound => NotFound(new { message = notFound.Message }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new { message = err.Message })
        };
    }

    [HttpGet("categorias")]
    public ActionResult<List<CategoriaDto>> GetCategorias() {
        var categorias = service.GetAllCategories();
        return Ok(categorias);
    }

    [HttpGet("categorias/{id:long}")]
    public ActionResult<CategoriaDto> GetCategoriaById(int id) {
        var categoria = service.GetByIdCategories(id);
        return Ok(categoria);
    }
}