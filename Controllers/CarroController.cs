using Loja.Api.Models;
using Loja.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarroController : ControllerBase
{
    private readonly LojaService _lojaService;

    public CarroController(LojaService lojaService)
    {
        _lojaService = lojaService;
    }
    
    // // // CARRO

    [HttpGet("{carroId:length(24)}")]
    public async Task<ActionResult<IEnumerable<Carro>>> GetCarro(string carroId)
    {
        var carro = await _lojaService.GetCarroByIdAsync(carroId);
        
        if (carro is null) return NotFound();
        
        return Ok(carro);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Carro>>> GetAllCarros()
    {
        var carros = await _lojaService.GetAllCarrosAsync();
        return Ok(carros);
    }
    
    [HttpGet("completo/{carroId:length(24)}")]
    public async Task<ActionResult<CarroCompleto>> GetCarroCompleto(string carroId)
    {
        var carro = await _lojaService.GetCarroCompleto(carroId);
        
        if (carro == default!) return NotFound();
        
        return Ok(carro);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateCarro([FromBody]Carro carro)
    {
        var isSuccess = await _lojaService.CreateCarroAsync(carro);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return Ok(carro.NomeCarro);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateCarro([FromBody]Carro carro)
    {
        var isSuccess = await _lojaService.UpdateCarroAsync(carro);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    [HttpDelete("{carroId:length(24)}")]
    public async Task<ActionResult> DeleteCarro(string carroId)
    {
        var isSuccess = await _lojaService.DeleteCarroAsync(carroId);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }
}