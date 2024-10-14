using Microsoft.AspNetCore.Identity;

namespace Teppi.Share.Entities;

public class User : IdentityUser
{
    public ICollection<UserRole>? UserRoles { get; set; }
}