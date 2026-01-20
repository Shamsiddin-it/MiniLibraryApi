using System;
using System.Net;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Services;

public class ProfileService(ApplicationDbContext applicationDbContext) : IProfileService
{
    private readonly ApplicationDbContext dbContext = applicationDbContext;
    public async Task<Response<string>> Add(int userId)
    {
        var profile = new Profile(){UserId = userId};
        dbContext.Profiles.Add(profile);
        await dbContext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "added");
    }

    public async Task<Response<List<Profile>>> GetAll()
    {
        return new Response<List<Profile>>(HttpStatusCode.OK, "ok", dbContext.Profiles.ToList());
    }
}
