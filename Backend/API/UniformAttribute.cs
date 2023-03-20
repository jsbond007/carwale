using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Carwale.Objects;

namespace Carwale.API
{
    /// <summary>
    /// If any action response has any errors then StatusCode will be returned 400, even if we are sending Ok Response from Controller Action
    /// </summary>
    public class UniformAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.Result is ObjectResult objectResult)
            {
                if (objectResult.StatusCode == 200)
                {
                    if (objectResult.Value is BaseResponse result && (result.HasError || result.Errors?.Count() > 0))
                    {
                        objectResult.StatusCode = 400;
                    }
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}
