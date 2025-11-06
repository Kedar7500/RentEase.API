using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace RentEase.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, 
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex) 
            {
                // create a unique identifier i.e error id
                var errorId = Guid.NewGuid().ToString();

                // log this exception into file
                logger.LogError($"{errorId} : error message {ex.Message}");

                // create a custom error response
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                // create a new custom error mode
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Somethimng went wrong , looking into this",
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
