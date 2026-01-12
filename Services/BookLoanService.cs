using System;
using System.Net;
using Dapper;
using WebApi.DTOs;
using WebApi.Interfaces;

namespace WebApi.Services;

public class BookLoanService(ApplicationDbContext applicationDbContext): IBookLoanService
{
    private readonly ApplicationDbContext dbContext = applicationDbContext;
    public async Task<Response<string>> Add(AddBookLoanDto bookLoanDto)
    {
        try
        {
            BookLoan bookLoan = new BookLoan
            {
                UserId = bookLoanDto.UserId,
                BookId = bookLoanDto.BookId,
                LoanDate = DateTime.Now
            };
            using var conn = dbContext.Connection();
            var query = "insert into bookLoans(bookid, userid, loandate) values(@bookid, @userid, @loandate)";
            var res = await conn.ExecuteAsync(query, new {bookid=bookLoan.BookId, userid = bookLoan.UserId, loandate=bookLoan.LoanDate});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not add!")
            : new Response<string>(HttpStatusCode.OK, "Added successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Delete(int bookLoanId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "delete from bookLoans where id=@id";
            var res = await conn.ExecuteAsync(query, new {id=bookLoanId});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not delete!")
            : new Response<string>(HttpStatusCode.OK, "Deleted successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<BookLoan>> GetBookLoanById(int bookLoanId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from bookLoans where id=@id";
            var res = await conn.QueryFirstOrDefaultAsync<BookLoan>(query, new {id=bookLoanId});
            return res==null
            ? new Response<BookLoan>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<BookLoan>(HttpStatusCode.OK, "Found successfully!", res);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<BookLoan>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<List<BookLoan>>> GetBookLoans()
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from bookLoans";
            var res = await conn.QueryAsync<BookLoan>(query);
            return res==null
            ? new Response<List<BookLoan>>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<List<BookLoan>>(HttpStatusCode.OK, "Found successfully!", res.ToList());
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<List<BookLoan>>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Update(BookLoan bookLoan)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "update BookLoans set bookid=@bookid, userid=@userid, loandate=@loandate where id=@id";
            var res = await conn.ExecuteAsync(query, new {bookid=bookLoan.BookId, userid=bookLoan.UserId, loandate=bookLoan.LoanDate, id=bookLoan.Id});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not update!")
            : new Response<string>(HttpStatusCode.OK, "Updated successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> ReturnBook(int bookLoanId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "update bookLoans set returndate=now() where id=@id";
            var res = await conn.ExecuteAsync(query, new {id=bookLoanId});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Not done!")
            : new Response<string>(HttpStatusCode.OK, "Book returned successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }
}
