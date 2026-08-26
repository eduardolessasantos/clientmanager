using AutoMapper;
using ClientManager.Api.DTOs;
using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IMapper _mapper;

    public ClientsController(IClienteService clienteService, IMapper mapper)
    {
        _clienteService = clienteService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna todos os clientes cadastrados.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        var dtos = _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        return Ok(dtos);
    }

    /// <summary>
    /// Retorna um cliente específico por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ClienteDto>> GetById(Guid id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente == null)
            return NotFound(new { message = $"Cliente com ID '{id}' não foi encontrado." });

        var dto = _mapper.Map<ClienteDto>(cliente);
        return Ok(dto);
    }

    /// <summary>
    /// Cadastra um novo cliente na plataforma.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Create([FromBody] CreateClienteDto createDto)
    {
        var clienteEntity = _mapper.Map<Cliente>(createDto);
        var id = await _clienteService.CriarClienteAsync(clienteEntity);
        clienteEntity.Id = id;

        var dto = _mapper.Map<ClienteDto>(clienteEntity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Atualiza um cliente existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteDto updateDto)
    {
        var clienteExistente = await _clienteService.ObterPorIdAsync(id);
        if (clienteExistente == null)
            return NotFound(new { message = $"Cliente com ID '{id}' não foi encontrado." });

        _mapper.Map(updateDto, clienteExistente);
        await _clienteService.EditarClienteAsync(clienteExistente);

        return NoContent();
    }

    /// <summary>
    /// Remove um cliente da plataforma.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var clienteExistente = await _clienteService.ObterPorIdAsync(id);
        if (clienteExistente == null)
            return NotFound(new { message = $"Cliente com ID '{id}' não foi encontrado." });

        await _clienteService.ExcluirClienteAsync(id);
        return NoContent();
    }
}
