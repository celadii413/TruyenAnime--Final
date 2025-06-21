using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TruyenAnime.Filters
{
    public class AuthorizeAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserRole") != "Admin")
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
            base.OnActionExecuting(context);
        }
    }
}
