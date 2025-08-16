using AppNum5.Models;
using AppNum5.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        var userDTO = await _userService.GetByIdAsync(id);

        if (userDTO == null)
            return NotFound();

        _logger.LogInformation("User getted: {UserId} {UserEmail}", userDTO.Id, userDTO.Email);
        return Ok(userDTO);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAll()
    {
        var usersDTO = await _userService.GetAllAsync();

        if (usersDTO.Count == 0)
            return NotFound();

        _logger.LogInformation("GetAll called: {TotalUsers}", usersDTO.Count);
        return Ok(usersDTO);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Post(PostUserDTO postUserDTO)
    {
        var userDTO = await _userService.CreateAsync(postUserDTO);

        _logger.LogInformation("User created: {UserId}, {Email}", userDTO.Id, userDTO.Email);
        return CreatedAtAction(nameof(GetById), new { id = userDTO.Id }, userDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _userService.DeleteAsync(id);

        if (!success)
            return NotFound();

        _logger.LogInformation("Deleted: {UserId}", id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PutUserDTO putUserDTO)
    {
        var success = await _userService.UpdateAsync(id, putUserDTO);

        if (!success)
            return NotFound();

        _logger.LogInformation("User Updated: {UserId}, {UserEmail}", id, putUserDTO.Email);
        return NoContent();
    }
}
