using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(IProfileService profileService) : ControllerBase
    {
        private readonly IProfileService service = profileService;
        [HttpPost]
        public async Task<Response<string>> Add(int userId)
        {
            return await service.Add(userId);
        }
        
        [HttpGet]
        public async Task<Response<List<Profile>>> GetAll()
        {
            return await service.GetAll();
        }
    }
}
