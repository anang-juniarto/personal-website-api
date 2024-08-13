using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebsiteAPI.Contexts;

namespace PersonalWebsiteAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationContext _context;

    public UserController(ApplicationContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var listUsers = await _context.User.ToListAsync();
        return StatusCode(StatusCodes.Status200OK, listUsers);
    }
}
