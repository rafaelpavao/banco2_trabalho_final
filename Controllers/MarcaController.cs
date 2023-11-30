using Loja.Api.Models;
using Loja.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MarcaController : ControllerBase
{
    private readonly LojaService _lojaService;

    public MarcaController(LojaService lojaService)
    {
        _lojaService = lojaService;
    }
    
    // // // MARCA

    [HttpGet("{marcaId:length(24)}")]
    public async Task<IActionResult> GetMarca(string marcaId)
    {
        var marca = await _lojaService.GetMarcaByIdAsync(marcaId);
        
        if (marca is null) return NotFound();
        
        return Ok(marca);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllMarcas()
    {
        var marcas = await _lojaService.GetAllMarcasAsync();
        return Ok(marcas);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMarca([FromBody]Marca marca)
    {
        var isSuccess = await _lojaService.CreateMarcaAsync(marca);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return Ok(marca.NomeMarca);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateMarca([FromBody]Marca marca)
    {
        var isSuccess = await _lojaService.UpdateMarcaAsync(marca);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    [HttpDelete("{marcaId:length(24)}")]
    public async Task<IActionResult> UpdateMarca(string marcaId)
    {
        var isSuccess = await _lojaService.DeleteMarcaAsync(marcaId);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    
}