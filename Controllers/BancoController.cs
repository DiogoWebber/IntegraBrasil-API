using System.ComponentModel.DataAnnotations;
using System.Net;
using IntegraBrasilApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraBrasilApi.Controllers{
    [ApiController]
    [Route("api/v1/[controller]")]
public class BancoController : ControllerBase
{
    public readonly IBancoService _BancoService;

    public BancoController(IBancoService bancoService)
    {
        _BancoService = bancoService;
    }

    [HttpGet("busca/todos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuscarTodos()
    {
        var response = await _BancoService.BuscarTodos();
        
        if(response.CodigoHttp == HttpStatusCode.OK){
            return Ok(response.DadosRetorno);
        }
        else{
            return StatusCode((int) response.CodigoHttp, response.ErroRetorno);
        }
    }
    
    [HttpGet("busca/{codigoBanco}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Buscar([RegularExpression("^[0-9]*$")]string codigoBanco)
    {
        var response = await _BancoService.BuscarBanco(codigoBanco);
        
        if(response.CodigoHttp == HttpStatusCode.OK){
            return Ok(response.DadosRetorno);
        }
        else{
            return StatusCode((int) response.CodigoHttp, response.ErroRetorno);
        }
    }
}}