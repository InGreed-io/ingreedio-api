using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace InGreedIoApi.Utils.Pagination
{
    public class PaginationFilter : IAsyncActionFilter
    {
        private PaginationOptions _options;

        public PaginationFilter(IOptions<PaginationOptions> options)
        {
            _options = options.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            if (context.Result is OkObjectResult okObjectResult && okObjectResult.Value is IPage page)
            {
                context.HttpContext.Response.Headers.Add(
                    _options.PageNumberHeaderName,
                    page.Metadata.PageNumber.ToString()
                );
                context.HttpContext.Response.Headers.Add(
                    _options.PageSizeHeaderName,
                    page.Metadata.PageSize.ToString()
                );
                context.HttpContext.Response.Headers.Add(
                    _options.IsLastHeaderName,
                    page.Metadata.IsLast.ToString()
                );

                context.Result = new OkObjectResult(page.Elements);
            }
        }
    }
}
