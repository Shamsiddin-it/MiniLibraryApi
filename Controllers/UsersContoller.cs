using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersContoller(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> Add(User User)
        {
            return await userService.Add(User);
        }

        [HttpPut]
        public async Task<Response<string>> Update(User User)
        {
            return await userService.Update(User);
        }

        [HttpDelete("{UserId}")]
        public async Task<Response<string>> Delete(int UserId)
        {
            return await userService.Delete(UserId);
        }

        [HttpGet]
        public async Task<Response<List<User>>> GetUsers()
        {
            return await userService.GetUsers();   
        }

        [HttpGet("{UserId}")]
        public async Task<Response<User>> GetUserById(int UserId)
        {
            return await userService.GetUserById(UserId);
        }

        [HttpGet("{userid}/loans")]
        public async Task<Response<UserWithLoans>> GetUserWithLoans(int userid)
        {
            return await userService.GetUserWithLoans(userid);
        }
    }
}