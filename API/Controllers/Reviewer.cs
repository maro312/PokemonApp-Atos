using Application.Contracts;
using Application.Dtos.Reviewers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class Reviewer : ControllerBase
{
    private readonly IReviewerAppService _appService;

    public Reviewer(IReviewerAppService appService)
    {
        _appService = appService;
    }

    /// <summary>
    /// Get all reviewers.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appService.GetAllAsync();
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors.FirstOrDefault());
    }

    /// <summary>
    /// Get a reviewer by its ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _appService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors.FirstOrDefault());
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new reviewer.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUpdateReviewerDto input)
    {
        var result = await _appService.CreateAsync(input);

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
    /// Update an existing reviewer.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateReviewerDto input)
    {
        var result = await _appService.UpdateAsync(input, id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors.FirstOrDefault());
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a reviewer by its ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _appService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors.FirstOrDefault());
        }

        return Ok(result.Value);
    }
}
