using Microsoft.AspNetCore.Identity;

namespace Teppi.Share.Entities;

public class UserRole : IdentityUserRole<string>
{
    public User? User { get; set; }
    public Role? Role { get; set; }
}