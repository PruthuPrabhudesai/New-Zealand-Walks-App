// This file is used to create a custom action filter that validates the model state before executing the action method. If the model state is invalid, it returns a BadRequestResult. This is useful for ensuring that the incoming request data is valid before processing it in the controller action.
// We're able to use validate attribute globally because of this file. Otherwise we'd have to check the validity of the model state in every action method. This is a good example of the DRY (Don't Repeat Yourself) principle in action.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)        // ASP.NET Core's model binding and validation pipeline populates ModelState automatically
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
