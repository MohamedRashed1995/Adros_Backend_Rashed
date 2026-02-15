namespace Adros.Shared.Exceptions
{
    public class StudentServiceException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
