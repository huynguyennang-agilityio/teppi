using Teppi.Share.Entities;

namespace Teppi.Share.DTOs.Responses;

public class UserResponseDTO
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string? Role { get; set; }
}