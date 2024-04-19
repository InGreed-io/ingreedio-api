using Microsoft.AspNetCore.Mvc.Filters;

namespace InGreedIoApi.Model.Exceptions
{
    public class InGreedIoExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InGreedIoException exception)
            {
                context.Result = exception.Result;
                context.ExceptionHandled = true;
            }
        }
    }
}
