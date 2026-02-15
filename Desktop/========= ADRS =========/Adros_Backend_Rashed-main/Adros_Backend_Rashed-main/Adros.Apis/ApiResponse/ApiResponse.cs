namespace Adros.Apis.ApiResponse
{
    public class ApiResponse<T>(int statusCode, string? message = null, T data = default!)
    {
        public int StatusCode { get; init; } = statusCode;
        public string? Message { get; init; } = message ?? ApiResponse<T>.GetDefaultMessageForStatusCode(statusCode);
        public T? Data { get; init; } = data;

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                400 => "A bad request was made.",
                401 => "Unauthorized access.",
                404 => "The requested resource was not found.",
                500 => "An internal server error has occurred.",
                _ => "An error occurred."
            };
        }
    }
}
