namespace Teppi.Share.DTOs.Requests;

public class RegisterRequestDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    public string? Role { get; set; }

}