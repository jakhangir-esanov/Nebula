namespace Nebula.Domain.Exceptions;

public class CustomException : Exception
{
    public int statusCode { get; set; }
    public CustomException(int statusCode, string message) : base(message)
    {
        this.statusCode = statusCode;
    } 
    
    public CustomException(int statusCode, string message, Exception innerException) : base(message, innerException)
    {
        this.statusCode = statusCode;
    }
}
