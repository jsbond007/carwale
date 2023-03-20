using Carwale.Objects;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Carwale.API
{
    /// <summary>
    /// Global level Error Handling Middleware, it will handle all kind of unhandled exception
    /// and will wrap it in as BaseErrorResponse
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {                
                await next(context);
            }
            catch (EntityValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, List<object> ex)
        {
            return SendExceptionAsync(context, HttpStatusCode.BadRequest, ex);
        }

        private static Task HandleExceptionAsync(HttpContext context, EntityValidationException ex)
        {
            return SendExceptionAsync(context, HttpStatusCode.BadRequest, ex.ApiResponse);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            /* Returns Exception Message and Stacktrack if Unhandled exception occors*/
            string message = ex.Message + "\r\n" + ex.InnerException?.Message;
            BaseErrorResponse errorResult = new BaseErrorResponse() { HasError = true, Message = message, StackTrace = ex.StackTrace };
            return SendExceptionAsync(context, HttpStatusCode.InternalServerError, errorResult);
        }

        private static Task SendExceptionAsync(HttpContext context, HttpStatusCode statusCode, object result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result, _serializerSettings));
        }
    }
}
