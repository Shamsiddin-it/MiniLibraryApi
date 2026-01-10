using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController(IBookLoanService bookLoanService) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> Add(BookLoan bookLoan)
        {
            return await bookLoanService.Add(bookLoan);
        }

        [HttpPut]
        public async Task<Response<string>> Update(BookLoan bookLoan)
        {
            return await bookLoanService.Update(bookLoan);
        }

        [HttpDelete("{bookloanId}")]
        public async Task<Response<string>> Delete(int bookloanId)
        {
            return await bookLoanService.Delete(bookloanId);
        }

        [HttpGet]
        public async Task<Response<List<BookLoan>>> GetBookLoans()
        {
            return await bookLoanService.GetBookLoans();   
        }

        [HttpGet("{bookloanId}")]
        public async Task<Response<BookLoan>> GetBookLoanById(int bookloanId)
        {
            return await bookLoanService.GetBookLoanById(bookloanId);
        }

        [HttpPut("{bookLoanId}")]
        public async Task<Response<string>> ReturnBook(int bookLoanId)
        {
            return await bookLoanService.ReturnBook(bookLoanId);
        }
    }
}
