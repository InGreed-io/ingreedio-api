using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace InGreedIoApi.Utils.Pagination
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PaginatedAttribute : ServiceFilterAttribute<PaginationFilter>
    { }

    public class PaginationFilter : IAsyncResultFilter
    {
        private PaginationOptions _options;

        public PaginationFilter(IOptions<PaginationOptions> options)
        {
            _options = options.Value;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_options.MoveMetadataToHeader && context.Result is ObjectResult objectResult && objectResult.Value is IPage page) 
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

                objectResult.Value = page.Elements;
            }

            await next();
        }
    }
}
