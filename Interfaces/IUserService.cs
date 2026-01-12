using System;
using WebApi.DTOs;

namespace WebApi.Interfaces;

public interface IUserService
{
    Task<Response<string>> Add(User user);
    Task<Response<string>> Update(User user);
    Task<Response<string>> Delete(int userId);
    Task<Response<List<User>>> GetUsers();
    Task<Response<User>> GetUserById(int userId);

    Task<Response<UserWithLoans>> GetUserWithLoans(int userId);
}
