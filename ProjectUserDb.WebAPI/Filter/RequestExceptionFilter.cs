using Evertrust.ResponseWrapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectUser.Services.Exceptions;

namespace ProjectUser.WebAPI.Filter
{
    public class RequestExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await Task.Run(() =>
            {
                //400 Bad Request
                if (context.Exception is UserNotFoundException)
                {
                    var failureResultOutputModel = new FailureResultOutputModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ApiVersion = "1.0.0",
                        Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                        Status = "ValidationError",
                        Errors = new List<FailureInformation>
                        {
                            new FailureInformation
                            {
                                ErrorCode = 400,
                                Message = "Bad Request",
                                Description = context.Exception.Message
                            }
                        }
                    };

                    context.Result = new BadRequestObjectResult(failureResultOutputModel);
                }

                //404 Not Found
                if (context.Exception is UserNotFoundException)
                {
                    var failureResultOutputModel = new FailureResultOutputModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ApiVersion = "1.0.0",
                        Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                        Status = "ValidationError",
                        Errors = new List<FailureInformation>
                        {
                            new FailureInformation
                            {
                                ErrorCode = 404,
                                Message = "Data Not Found",
                                Description = context.Exception.Message
                            }
                        }
                    };
                    
                    context.Result = new NotFoundObjectResult(failureResultOutputModel);
                }
            });
        }
    }
}