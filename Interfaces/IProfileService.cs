using System;
using WebApi.Entities;

namespace WebApi.Interfaces;

public interface IProfileService
{
    Task<Response<string>> Add(int userId);
    Task<Response<List<Profile>>> GetAll();
}
