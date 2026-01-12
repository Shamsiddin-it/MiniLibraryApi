using System;
using WebApi.DTOs;

namespace WebApi.Interfaces;

public interface IBookLoanService
{
    Task<Response<string>> Add(AddBookLoanDto bookLoan);
    Task<Response<string>> Update(BookLoan bookLoan);
    Task<Response<string>> Delete(int bookLoanId);
    Task<Response<List<BookLoan>>> GetBookLoans();
    Task<Response<BookLoan>> GetBookLoanById(int bookLoanId);
    Task<Response<string>> ReturnBook(int bookLoanId);
}
