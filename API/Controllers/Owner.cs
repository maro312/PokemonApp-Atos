using Application.Contracts;
using Application.Dtos.Owners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class Owner : ControllerBase
{
    private readonly IOwnerAppService _ownerAppService;

    public Owner(IOwnerAppService ownerAppService)
    {
        _ownerAppService = ownerAppService;
    }

    /// <summary>
    /// Get all owners.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _ownerAppService.GetAllAsync();
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    /// <summary>
    /// Get an owner by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _ownerAppService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new owner.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUpdateOwnerDto input)
    {
        var result = await _ownerAppService.CreateAsync(input);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id },
            result.Value
        );
    }

    /// <summary>
    /// Update an existing owner.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateOwnerDto input)
    {
        var result = await _ownerAppService.UpdateAsync(input, id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete an owner by ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _ownerAppService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Value);
    }
}
