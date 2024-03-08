using Application.Base.Exceptions;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace WebAPI.Base.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            response.ContentType = "application/json";
            string result = string.Empty;
            var serialieOption = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            switch (ex)
            {
                case CrudException e:
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize(new { message = e?.Message }, serialieOption);
                        break;
                    }
                default:
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result = JsonSerializer.Serialize(new { message = "Something went wrong" }, serialieOption);
                        break;
                    }
            }
            await context.Response.WriteAsync(result);
        }
    }
}
