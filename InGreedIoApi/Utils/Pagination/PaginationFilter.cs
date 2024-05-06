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
            if (context.Result is ObjectResult objectResult && objectResult.Value is IPage page)
            {
                if (_options.MoveMetadataToHeader) 
                {
                    context.HttpContext.Response.Headers.Append(
                        _options.PageIndexHeaderName,
                        page.Metadata.PageIndex.ToString()
                    );
                    context.HttpContext.Response.Headers.Append(
                        _options.PageSizeHeaderName,
                        page.Metadata.PageSize.ToString()
                    );
                    context.HttpContext.Response.Headers.Append(
                        _options.PageCountHeaderName,
                        page.Metadata.PageCount.ToString()
                    );

                    objectResult.Value = page.Contents;
                }
                else
                {
                    objectResult.Value = new 
                    { 
                        Contents = page.Contents, 
                        Metadata = page.Metadata 
                    };
                }
            }

            await next();
        }
    }
}
