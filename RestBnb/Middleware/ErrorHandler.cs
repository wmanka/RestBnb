using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestBnb.API.Extensions;

namespace RestBnb.API.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            string result;

            if (ex is ValidationException exception)
            {
                code = HttpStatusCode.BadRequest;
                var errorResponse = new ErrorR();

                foreach (var error in exception.Errors)
                {
                    errorResponse.Errors.Add(
                        new ErrorM { Field = error.PropertyName, Message = error.ErrorMessage });
                }

                result = JsonConvert.SerializeObject(errorResponse);
            }
            else result = JsonConvert.SerializeObject(new { error = ex.Message });

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}