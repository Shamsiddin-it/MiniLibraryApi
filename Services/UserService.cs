// using System;
// using System.Net;
// using System.Net.Http.Headers;
// using System.Text;
// using Dapper;
// using WebApi.DTOs;
// using WebApi.Interfaces;

// namespace WebApi.Services;

// public class UserService(ApplicationDbContext applicationDbContext, ILogger<UserService> logger): IUserService
// {
//     private readonly ApplicationDbContext dbContext = applicationDbContext;
//     private readonly ILogger<UserService> _logger = logger;
//     public async Task<Response<string>> Add(UserDto userDto)
//     {
//         try
//         {
//             _logger.LogInformation("Starting user registration");
//             User user = new User
//             {
//                 FullName = userDto.FullName,
//                 Email = userDto.Email,
//                 RegisteredAt = DateTime.Now
//             };
//             using var conn = dbContext.Connection();
//             var query = "insert into users(fullname, email, registeredat) values(@fullname, @email, @registeredat)";
//             var res = await conn.ExecuteAsync(query, new {fullname = user.FullName, email=user.Email, registeredat=user.RegisteredAt});
//             return res==0
//             ? new Response<string>(HttpStatusCode.InternalServerError, "Could not add!")
//             : new Response<string>(HttpStatusCode.OK, "Added successfully!");
//         }
//         catch(Exception ex)
//         {
//             System.Console.WriteLine(ex);
//             return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
//         }
//     }

//     public async Task<Response<string>> Delete(int userId)
//     {
//         try
//         {
//             using var conn = dbContext.Connection();
//             var query = "delete from users where id=@id";
//             var res = await conn.ExecuteAsync(query, new {id=userId});
//             return res==0
//             ? new Response<string>(HttpStatusCode.InternalServerError, "Could not delete!")
//             : new Response<string>(HttpStatusCode.OK, "Deleted successfully!");
//         }
//         catch(Exception ex)
//         {
//             System.Console.WriteLine(ex);
//             return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
//         }
//     }

//     public async Task<Response<UserDto>> GetUserById(int userId)
//     {
//         try
//         {
//             using var conn = dbContext.Connection();
//             var query = "select * from users where id=@id";
//             var res = await conn.QueryFirstOrDefaultAsync<User>(query, new {id=userId});
//             if (res != null)
//             {
//                 UserDto userDto = new UserDto
//                 {
//                     FullName = res.FullName,
//                     Email = res.Email
//                 };
//                 return new Response<UserDto>(HttpStatusCode.OK, "The user with that id", userDto);
//             }
//             else
//             {
//                 return new Response<UserDto>(HttpStatusCode.NotFound, "User not found!");
//             }
//         }
//         catch(Exception ex)
//         {
//             System.Console.WriteLine(ex);
//             return new Response<UserDto>(HttpStatusCode.InternalServerError, $"Something went wrong!");
//         }
//     }

//     public async Task<Response<List<User>>> GetUsers()
//     {
//         try
//         {
//             using var conn = dbContext.Connection();
//             var query = "select * from users";
//             var res = await conn.QueryAsync<User>(query);
//             return res==null
//             ? new Response<List<User>>(HttpStatusCode.InternalServerError, "Not found!")
//             : new Response<List<User>>(HttpStatusCode.OK, "Found successfully!", res.ToList());
//         }
//         catch(Exception ex)
//         {
//             System.Console.WriteLine(ex);
//             return new Response<List<User>>(HttpStatusCode.InternalServerError, $"Something went wrong!");
//         }
//     }

//     public async Task<Response<string>> Update(UpdateUserDto userDto)
//     {
//         try
//         {
//             User user = new User
//             {
//                 Id = userDto.Id,
//                 FullName = userDto.FullName,
//                 Email = userDto.Email
//             };
//             using var conn = dbContext.Connection();
//             var query = "update users set fullname=@fullname, email=@email where id=@id";
//             var res = await conn.ExecuteAsync(query, new {fullname = user.FullName, email=user.Email, id=user.Id});
//             return res==0
//             ? new Response<string>(HttpStatusCode.InternalServerError, "Could not update!")
//             : new Response<string>(HttpStatusCode.OK, "Updated successfully!");
//         }
//         catch(Exception ex)
//         {
//             System.Console.WriteLine(ex);
//             return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
//         }
//     }

//     public async Task<Response<UserWithLoans>> GetUserWithLoans(int userId)
//     {
//         using var conn = dbContext.Connection();
//         var query = "select u.fullname, u.email from users u where u.id=@id";
//         var query2 = "select * from bookloans where userid=@id";
//         var res = await conn.QueryFirstOrDefaultAsync<UserWithLoans>(query, new {id=userId});
//         var res2 = await conn.QueryAsync<BookLoan>(query2, new {id=userId});
        
//         if (res != null)
//         {
//             foreach(var item in res2)
//             {
//                 res.BookLoans.Add(item);
//             }
//             UserWithLoans userWithLoans = new UserWithLoans
//             {
//                 FullName = res.FullName,
//                 Email = res.Email,
//                 BookLoans = res.BookLoans
//             };
//             return new Response<UserWithLoans>(HttpStatusCode.OK, "found", userWithLoans);
//         }
//         else
//         {
//             return new Response<UserWithLoans>(HttpStatusCode.InternalServerError, "could not find anything");
//         }
//     }

// }
