using Adros.Apis.Middlewares;

namespace Adros.Apis.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            // Exception Handling Middleware should be first
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Request Logging Middleware (if implemented)
            app.UseMiddleware<RequestLoggingMiddleware>();

            // HTTPS Redirection
            app.UseHttpsRedirection();

            // Static Files
            app.UseStaticFiles();

            // Routing
            app.UseRouting();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // CORS (if needed)
            app.UseCors("AllowAll");

            // Map Controllers
            app.MapControllers();

            return app;
        }
    }
}
