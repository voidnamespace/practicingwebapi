using Microsoft.EntityFrameworkCore;
using AppNum5.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<UsersController> _logger;
    public UsersController(AppDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {

        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var userDTO = new UserDTO 
        { 
            Id = user.Id, 
            Email = user.Email, 
            Name = user.Name 
        };
        _logger.LogInformation("User getted: {UserId} {UserEmail}", user.Id , user.Email);
        return Ok(userDTO);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        if (users.Count == 0) return NotFound();
     
        
        var usersDTO = users
        .Select(u => new UserDTO
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
        })
        .ToList();

        _logger.LogInformation("GetAll called: {TotalUsers}", users.Count);


        return Ok(usersDTO);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Post(PostUserDTO postuserDTO)
    {
        var user = new User 
        {
            Name = postuserDTO.Name, 
            Email = postuserDTO.Email, 
            Password = postuserDTO.Password 
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userDTO = new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
        };
       
        _logger.LogInformation("User created: {UserId}, {Email}", user.Id, user.Email);
        
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDTO);    
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete (int id)
    {
        var deluser = await _context.Users.FindAsync(id);

        if (deluser == null)
        {
            return NotFound();
        }
        _context.Users.Remove(deluser);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Deleted: {UserId}", deluser.Id);

        return NoContent(); 

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PutUserDTO putuserDTO)

    {
        var refreshuser = await _context.Users.FindAsync(id);
        
        if (refreshuser == null)
        {
            return NotFound();
        }

        refreshuser.Name = putuserDTO.Name;
        refreshuser.Email = putuserDTO.Email;  
       
        await _context.SaveChangesAsync();

        _logger.LogInformation("User Updated: {UserId}, {UserEmail}",id, refreshuser.Email);

        return NoContent();

    }
}