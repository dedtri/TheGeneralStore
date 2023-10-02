using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TheGeneralStore.Backend.WebAPI.Persistence.Services;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.Repositories;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CustomerRepository customerRepository;
    private readonly TokenService tokenService;
    private readonly UnitOfWork unitOfWork;

    public AuthController(CustomerRepository customerRepository, TokenService tokenService, UnitOfWork unitOfWork)
    {
        this.customerRepository = customerRepository;
        this.tokenService = tokenService;
        this.unitOfWork = unitOfWork;
    }
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody] Customer login)
    {
        if (login is null)
        {
            return BadRequest("Invalid client request");
        }

        var user = this.customerRepository.ConfirmLogin(login);

        if (user is null)
            return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login.Email),
        };
        if (user.Role == "Admin")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, "User"));
        }
        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        this.unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return Ok(new AuthenticatedResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken
        });
    }

    [HttpPost, Route("register")]
    public async Task<IActionResult> Create(Customer login)
    {
        login.Role = "User";

        this.customerRepository.Add(login);

        await this.unitOfWork.SaveChangesAsync();

        return Ok(login);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetByName(string email)
    {
        var login = await this.customerRepository.GetByNameAsync(email);
        return login == null ? NotFound() : Ok(login);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var login = await this.customerRepository.GetAsync(id);
        return login == null ? NotFound() : Ok(login);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Customer login)
    {
        if (id != login.Id) return BadRequest();

        this.customerRepository.Update(login);

        await this.unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}