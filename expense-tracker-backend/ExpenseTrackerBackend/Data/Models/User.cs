using Microsoft.AspNetCore.Identity;
using Npgsql;

namespace ExpenseTrackerBackend.Data.Models;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        Id = Guid.NewGuid();
        SecurityStamp = Guid.NewGuid().ToString();
    }
    
    public User(string username) : base(username)
    {
        Id = Guid.NewGuid();
        SecurityStamp = Guid.NewGuid().ToString();
    }
}

public class Role : IdentityRole<Guid>
{
}