using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using SelfFinance.Core.Exceptions;

namespace SelfFinanceAPI.Filters
{
    public class ApiExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            ObjectResult result;
            int statusCode;

            switch (exception)
            {
                case NotFoundException notFound:
                    statusCode = StatusCodes.Status404NotFound;
                    result = new ObjectResult(new { error = notFound.Message });
                    break;

                case ValidationException validation:
                    statusCode = StatusCodes.Status400BadRequest;
                    result = new ObjectResult(new { error = validation.Message });
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    result = new ObjectResult(new { error = "An unexpected error occurred." });
                    break;
            }

            result.StatusCode = statusCode;
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
