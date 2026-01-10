using System.ComponentModel;
using System.Net;

public class Response<T>
{
    public HttpStatusCode httpStatusCode {get; set;}
    public string? Description {get; set;}=null!;
    public List<T>? Data {get; set;}=[];
    public Response(HttpStatusCode httpStatusCode, string message, T data)
    {
        this.httpStatusCode = httpStatusCode;
        Description = message;
        Data.Add(data);
    }
    public Response(HttpStatusCode httpStatusCode, string message)
    {
        this.httpStatusCode = httpStatusCode;
        Description = message;
    }
}