using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventApiDemo.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Filtering: An error occurred while processing your request.");
            context.Result = new JsonResult(new { success = false, message = "Filtering: An error occurred while processing your request. Please try again later." })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}