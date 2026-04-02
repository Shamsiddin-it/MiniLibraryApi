using Microsoft.EntityFrameworkCore;

namespace WebApi.Tests.Helpers;

internal static class TestApplicationDbContextFactory
{
    public static ApplicationDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new ApplicationDbContext(options);
    }
}
