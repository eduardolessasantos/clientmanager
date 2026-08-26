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
public class ProjectsController : ControllerBase
{
    private readonly IProjetoService _projetoService;
    private readonly IMapper _mapper;

    public ProjectsController(IProjetoService projetoService, IMapper mapper)
    {
        _projetoService = projetoService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna todos os projetos cadastrados.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProjetoDto>>> GetAll()
    {
        var projetos = await _projetoService.ObterTodosAsync();
        var dtos = _mapper.Map<IEnumerable<ProjetoDto>>(projetos);
        return Ok(dtos);
    }

    /// <summary>
    /// Retorna um projeto específico por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProjetoDto>> GetById(Guid id)
    {
        var projeto = await _projetoService.ObterPorIdAsync(id);
        if (projeto == null)
            return NotFound(new { message = $"Projeto com ID '{id}' não foi encontrado." });

        var dto = _mapper.Map<ProjetoDto>(projeto);
        return Ok(dto);
    }

    /// <summary>
    /// Retorna todos os projetos associados a um cliente específico.
    /// </summary>
    [HttpGet("/api/clients/{clientId:guid}/projects")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProjetoDto>>> GetProjectsByClientId(Guid clientId)
    {
        var projetos = await _projetoService.ObterTodosAsync();
        var projetosDoCliente = projetos.Where(p => p.ClienteId == clientId);
        var dtos = _mapper.Map<IEnumerable<ProjetoDto>>(projetosDoCliente);
        return Ok(dtos);
    }

    /// <summary>
    /// Cadastra um novo projeto.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProjetoDto>> Create([FromBody] CreateProjetoDto createDto)
    {
        var projetoEntity = _mapper.Map<Projeto>(createDto);
        var id = await _projetoService.CriarProjetoAsync(projetoEntity);
        projetoEntity.Id = id;

        var dto = _mapper.Map<ProjetoDto>(projetoEntity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Atualiza um projeto existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjetoDto updateDto)
    {
        var projetoExistente = await _projetoService.ObterPorIdAsync(id);
        if (projetoExistente == null)
            return NotFound(new { message = $"Projeto com ID '{id}' não foi encontrado." });

        _mapper.Map(updateDto, projetoExistente);
        await _projetoService.EditarProjetoAsync(projetoExistente);

        return NoContent();
    }

    /// <summary>
    /// Remove um projeto.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var projetoExistente = await _projetoService.ObterPorIdAsync(id);
        if (projetoExistente == null)
            return NotFound(new { message = $"Projeto com ID '{id}' não foi encontrado." });

        await _projetoService.ExcluirProjetoAsync(id);
        return NoContent();
    }
}
