using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teppi.Share.DTOs.Requests;
using Teppi.Share.DTOs.Responses;
using Teppi.WebApi.Extensions;

namespace Teppi.WebApi.Controllers.V1;

public class UsersController(
    IUserService userService,
    IMapper mapper,
    ILogger<UsersController> logger
) : BaseApiController
{
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequestDTO requestDto)
    {
        var result = await userService.CreateUser(requestDto);
        var value = mapper.Map<UserResponseDTO>(
            result.Value is null ? null : (UserResponseDTO)result.Value
        );

        return result.IsSuccess ? Ok(value) : result.ToProblemDetails();
    }

    [HttpPost("set-role")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> SetRoleAsync(SetRoleUserDTO setRoleUserDTO)
    {
        logger.LogInformation("Set role with UserId {@UserId}", setRoleUserDTO.UserId);
        var result = await userService.SetRoleUser(setRoleUserDTO.UserId, setRoleUserDTO.Role);
        return result.IsSuccess
            ? Ok(
                new ResultSuccessDTO { Title = "Success" }
            )
            : result.ToProblemDetails();
    }
    
    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetAllUsers([FromQuery] QueryParameters queryParam)
    {
        var result = await userService.GetAllUser(queryParam);
        var value = mapper.Map<IEnumerable<UserResponseDTO>>(
            result.Value is null ? null : (IEnumerable<UserResponseDTO>)result.Value
        );

        return result.IsSuccess ? Ok(value) : result.ToProblemDetails();
    }
    
    [HttpGet("user-info")]
    [Authorize()]
    public async Task<IActionResult> GetUserInfo()
    {
        var result = await userService.GetUserInfo();
        UserResponseDTO? value = null;
        if (result.Value is User userEntity)
        {
            value = mapper.Map<UserResponseDTO>(userEntity);
        }

        return result.IsSuccess ? Ok(value) : result.ToProblemDetails();
    }

}