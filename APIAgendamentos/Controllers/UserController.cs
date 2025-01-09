using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<IdentityUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }


[HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
    {
        var user = new IdentityUser { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok("Usuário registrado com sucesso!");
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("add-role")]
    public async Task<IActionResult> AddRole([FromBody] RoleDto model)
    {
        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Role));
        }

        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null) return NotFound("Usuário não encontrado.");

        await _userManager.AddToRoleAsync(user, model.Role);
        return Ok($"Role {model.Role} adicionada ao usuário {model.Username}.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null)
        {
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            Console.WriteLine($"User found: {user.UserName}, Password valid: {isPasswordValid}");

            if (isPasswordValid)
            {
                var token = await GenerateToken(user);
                return Ok(new { token });
            }
        }
        Console.WriteLine("Usuário ou senha inválidos.");
        return Unauthorized("Usuário ou senha inválidos.");
    }


    private async Task<string> GenerateToken(IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class UserRegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RoleDto
{
    public string Username { get; set; }
    public string Role { get; set; }
}

public class UserLoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
