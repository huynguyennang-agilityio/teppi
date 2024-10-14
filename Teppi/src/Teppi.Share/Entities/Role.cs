using Microsoft.AspNetCore.Identity;

namespace Teppi.Share.Entities;

public class Role : IdentityRole
{
    public ICollection<UserRole>? UserRoles { get; set; }
}
