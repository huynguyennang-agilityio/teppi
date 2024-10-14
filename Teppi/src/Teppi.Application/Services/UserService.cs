using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Teppi.Application.Interfaces;
using Teppi.Application.Models;
using Teppi.Data.Persistence;
using Teppi.Share.DTOs.Requests;
using Teppi.Share.DTOs.Responses;
using Teppi.Share.Entities;
using Teppi.Share.Enums;

namespace Teppi.Application.Services;

public class UserService(
    TeppiDbContext context,
    ILogger<UserService> logger,
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    public async Task<Result> CreateUser(RegisterRequestDTO request)
    {
        logger.LogInformation("Register user with email {@email}", request.Email);

        // Check whether email exists
        var user = await userManager.FindByEmailAsync(request.Email);

        // Throw exception if user already registered
        if (user != null)
        {
            throw new NotImplementedException();
        }

        // Create new user
        user = new User
        {
            Email = request.Email,
            UserName = request.Email
        };
        var createResult = await userManager.CreateAsync(user, request.Password);

        // Throw exception if any errors during creation
        if (!createResult.Succeeded)
        {
            foreach (var err in createResult.Errors)
            {
                throw new NotImplementedException();
            }
        }
        
        // Add "User" role to user
        var addRoleResult = await userManager.AddToRoleAsync(user, request.Role ?? UserRoles.User);

        // Throw exception if any errors during adding role
        if (!addRoleResult.Succeeded)
        {
            foreach (var err in addRoleResult.Errors)
            {
                throw new NotImplementedException();
            }
        }

        logger.LogInformation("Register user with email {@email} successfully", request.Email);
        UserResponseDTO result = new UserResponseDTO
        {
            Email = user.Email,
            Role = request.Role ?? UserRoles.User
        }
        ;
        return Result.Success(result);

    }

    public async Task<Result> SetRoleUser(string id, string role)
    {
        var user = await userManager.FindByIdAsync(id);

        // Add "User" role to user
        var addRoleResult = await userManager.AddToRoleAsync(user, role ?? UserRoles.User);
        return Result.Success(addRoleResult);
    }

    public async Task<Result> UpdateUser(string id, User user)
    {
        // TODO: Implement update user
        return Result.Failure(Error.None);
    }

    public async Task<Result> DeleteUser(string id)
    {
        // TODO: Implement delete user
        return Result.Failure(Error.None);
    }

    public async Task<Result> GetAllUser(QueryParameters queryParam)
    {
        var users = userManager.Users.ToList();
        return Result.Success(users);
    }

    public async Task<Result> GetUserDetail(string id)
    {
        var user = await context.Users.FindAsync(id);
        return Result.Success(user);
    }

    public async Task<Result> GetUserInfo()
    { 
        var userId = GetUserId();
        var user = await context.Users.FindAsync(userId);
        return Result.Success(user);
    }

    private string? GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
