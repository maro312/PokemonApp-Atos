using Application.Contracts;
using Application.Dtos.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class Country : ControllerBase
{
    private readonly ICountryAppService _countryAppService;

    public Country(ICountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }

    /// <summary>
    /// Get all countries.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _countryAppService.GetAllAsync();
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors.FirstOrDefault());
    }

    /// <summary>
    /// Get a country by its ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _countryAppService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors.FirstOrDefault());
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new country.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUpdateCountryDto input)
    {
        var result = await _countryAppService.CreateAsync(input);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors.FirstOrDefault());
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id },
            result.Value
        );
    }

    /// <summary>
    /// Update an existing country.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateCountryDto input)
    {
        var result = await _countryAppService.UpdateAsync(input, id);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a country by its ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _countryAppService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors.FirstOrDefault());
        }

        return Ok(result.Value);
    }
}
