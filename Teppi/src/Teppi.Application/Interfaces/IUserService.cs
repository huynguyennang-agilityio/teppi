using System.Security.Claims;
using Teppi.Application.Models;
using Teppi.Share.DTOs.Requests;
using Teppi.Share.DTOs.Responses;
using Teppi.Share.Entities;

namespace Teppi.Application.Interfaces;

public interface IUserService
{
    Task<Result> CreateUser(RegisterRequestDTO request);
    Task<Result> SetRoleUser(string id, string role);
    Task<Result> UpdateUser(string id, User user);
    Task<Result> DeleteUser(string id);
    Task<Result> GetAllUser(QueryParameters queryParam);
    Task<Result> GetUserDetail(string id);
    Task<Result> GetUserInfo();

}