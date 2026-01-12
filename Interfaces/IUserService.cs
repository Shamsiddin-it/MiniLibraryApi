using System;
using WebApi.DTOs;

namespace WebApi.Interfaces;

public interface IUserService
{
    Task<Response<string>> Add(UserDto user);
    Task<Response<string>> Update(UpdateUserDto user);
    Task<Response<string>> Delete(int userId);
    Task<Response<List<User>>> GetUsers();
    Task<Response<UserDto>> GetUserById(int userId);

    Task<Response<UserWithLoans>> GetUserWithLoans(int userId);
}
