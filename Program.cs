using WebApi.Interfaces;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookLoanService, BookLoanService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(config=>
    {
        config.AddConsole();
        config.SetMinimumLevel(LogLevel.Information);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
// app.Run(async context=> await context.Response.WriteAsync("The response gone"));
// app.Use(async (context, next)=>
// {
//     System.Console.WriteLine("Before response");
//     await next.Invoke();
//     System.Console.WriteLine("After response");
// });
app.UseMiddleware<RequestTimeMiddleware>();
app.Run();

