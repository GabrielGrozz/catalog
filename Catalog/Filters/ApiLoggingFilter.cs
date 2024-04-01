using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
