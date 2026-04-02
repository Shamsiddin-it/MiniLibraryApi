using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Services;
using WebApi.Tests.Helpers;

namespace WebApi.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task Add_ShouldPersistBook_AndReturnOkResponse()
    {
        await using var context = TestApplicationDbContextFactory.CreateContext(nameof(Add_ShouldPersistBook_AndReturnOkResponse));
        context.Authors.Add(new Author
        {
            Id = 1,
            FullName = "Test Author",
            Country = "TJ"
        });
        await context.SaveChangesAsync();

        var service = new BookService(context, NullLogger<BookService>.Instance);
        var dto = new BookDto
        {
            Title = "Clean Code",
            PublishedYear = 2008,
            Genre = "Programming",
            AuhtorId = 1
        };

        var response = await service.Add(dto);

        Assert.Equal(HttpStatusCode.OK, response.httpStatusCode);
        Assert.Equal("Added ok", response.Description);
        Assert.Single(context.Books);
        Assert.Equal("Clean Code", context.Books.Single().Title);
    }

    [Fact]
    public async Task GetBookById_WhenBookMissing_ShouldReturnNotFound()
    {
        await using var context = TestApplicationDbContextFactory.CreateContext(nameof(GetBookById_WhenBookMissing_ShouldReturnNotFound));
        var service = new BookService(context, NullLogger<BookService>.Instance);

        var response = await service.GetBookById(999);

        Assert.Equal(HttpStatusCode.NotFound, response.httpStatusCode);
        Assert.Equal("not found", response.Description);
        Assert.Empty(response.Data!);
    }

    [Fact]
    public async Task GetBooks_ShouldApplyFilter_AndPaginationDefaults()
    {
        await using var context = TestApplicationDbContextFactory.CreateContext(nameof(GetBooks_ShouldApplyFilter_AndPaginationDefaults));
        context.Books.AddRange(
            new Book { Id = 1, Title = "C# Basics", Genre = "Programming", PublishedYear = 2024, AuthorId = 1 },
            new Book { Id = 2, Title = "C# Advanced", Genre = "Programming", PublishedYear = 2024, AuthorId = 1 },
            new Book { Id = 3, Title = "Java Basics", Genre = "Programming", PublishedYear = 2023, AuthorId = 1 });
        await context.SaveChangesAsync();

        var service = new BookService(context, NullLogger<BookService>.Instance);

        var result = await service.GetBooks(
            new BookFilter { Title = "C#" },
            new PagedQuery { Page = 0, PageSize = 0 });

        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.TotalPages);
        Assert.True(result.HasPrevious is false);
        Assert.True(result.HasNext is false);
        Assert.Equal(new[] { 1, 2 }, result.Items.Select(x => x.Id).ToArray());
    }
}
