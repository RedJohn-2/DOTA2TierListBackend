using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Text.Json;

namespace DOTA2TierList.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound, ex.Message);
            }
            catch (DuplicateException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (AuthenticationException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context, 
            string exMessage, 
            HttpStatusCode statusCode,
            string message)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorResponse errorResponse = new((int)statusCode, message);

            string result = errorResponse.ToString();

            await response.WriteAsJsonAsync(result);
        }
    }
}
