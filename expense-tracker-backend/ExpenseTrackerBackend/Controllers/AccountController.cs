using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerBackend.Controllers;

[ApiController()]
[Route("[controller]/[action]")]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly ExpenseTrackerDbContext _db;

    public AccountController(SignInManager<User> signInManager, ExpenseTrackerDbContext db)
    {
        _signInManager = signInManager;
        _db = db;
    }

    [HttpPost(Name = nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
        if (result.Succeeded)
        {
            return Ok();
        }

        return Unauthorized();
    }

    [HttpPost(Name = nameof(Logout))]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost(Name = nameof(Register))]
    public async Task<IResult> Register([FromBody] RegisterRequest request)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();
        var result = await _signInManager.UserManager.CreateAsync(new User(request.Username), request.Password);
        if (result.Succeeded)
        {
            await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
            await transaction.CommitAsync();
            return TypedResults.Ok();
        }

        return TypedResults.Problem(new ProblemDetails
        {
            Title = "Registration failed", Detail = string.Join(";", result.Errors.Select(x => x.Description))
        });
    }

    [HttpGet(Name = nameof(GetMe))]
    [Authorize]
    [Produces(typeof(UserInfoDto))]
    public async Task<IResult> GetMe()
    {
        var user = await _signInManager.UserManager.GetUserAsync(User);

        if (user is null)
        {
            return TypedResults.Unauthorized();
        }

        return TypedResults.Ok(new UserInfoDto(user.Id, user.UserName!));
    }
}

public record struct LoginRequest(string Username, string Password);

public record struct RegisterRequest(string Username, string Password);

public record struct UserInfoDto(Guid Id, string Username);