using Evertrust.Core.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectUser.WebAPI.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logHelperFactory = context.HttpContext.RequestServices.GetRequiredService<ILogHelperFactory>();
            var action = context.RouteData.Values["action"]?.ToString();
            var controller = context.RouteData.Values["controller"]?.ToString();
            var logHelper = logHelperFactory.Create($"{controller}/{action}");

            logHelper.WriteException(LogLevel.Error, context.Exception);
        }
    }
}