public class BookLoan
{
    public int Id {get; set;}
    public int BookId {get; set;}
    public int UserId {get; set;}
    public DateTime LoanDate {get; set;}=DateTime.UtcNow;
    public DateTime? ReturnDate {get; set;}
}