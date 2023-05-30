using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly Lazy<Task<User>> _currentUser;

    public CurrentUserService(ExpenseTrackerDbContext db, IHttpContextAccessor httpContext)
    {
        _currentUser = new Lazy<Task<User>>(async () =>
        {
            var user = await db.Users.FirstOrDefaultAsync(x =>
                x.Id == Guid.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return user ?? throw new Exception("User not found");
        });
    }

    public async Task<User> GetCurrentUser()
    {
        return await _currentUser.Value;
    }
}

public interface ICurrentUserService
{
    Task<User> GetCurrentUser();
}