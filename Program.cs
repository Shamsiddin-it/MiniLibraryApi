using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Middlewares;
using WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Diagnostics;
// using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
});
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
// builder.Services.AddScoped<IBookLoanService, BookLoanService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(config=>
    {
        config.AddConsole();
        config.SetMinimumLevel(LogLevel.Information);
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

var applyMigrationsOnStartup = builder.Configuration.GetValue("ApplyMigrationsOnStartup", true);
var enableSwagger = builder.Configuration.GetValue("EnableSwagger", true);

if (applyMigrationsOnStartup)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (enableSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestTimeMiddleware>();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapControllers();
// app.Run(async context=> await context.Response.WriteAsync("The response gone"));
// app.Use(async (context, next)=>
// {
//     System.Console.WriteLine("Before response");
//     await next.Invoke();
//     System.Console.WriteLine("After response");
// });
app.Run();

