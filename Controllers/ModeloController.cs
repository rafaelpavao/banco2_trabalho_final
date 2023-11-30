using Loja.Api.Models;
using Loja.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeloController : ControllerBase
{
    private readonly LojaService _lojaService;

    public ModeloController(LojaService lojaService)
    {
        _lojaService = lojaService;
    }
    
    // // // MODELO

    [HttpGet("{modeloId:length(24)}")]
    public async Task<IActionResult> GetModelo(string modeloId)
    {
        var modelo = await _lojaService.GetModeloByIdAsync(modeloId);
        
        if (modelo is null) return NotFound();
        
        return Ok(modelo);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllModelos()
    {
        var modelos = await _lojaService.GetAllModelosAsync();
        return Ok(modelos);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateModelo([FromBody]Modelo modelo)
    {
        var isSuccess = await _lojaService.CreateModeloAsync(modelo);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return Ok(modelo.NomeModelo);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateModelo([FromBody]Modelo modelo)
    {
        var isSuccess = await _lojaService.UpdateModeloAsync(modelo);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    [HttpDelete("{modeloId:length(24)}")]
    public async Task<IActionResult> UpdateModelo(string modeloId)
    {
        var isSuccess = await _lojaService.DeleteModeloAsync(modeloId);
        if (!isSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }
}