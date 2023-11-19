using LearningCenter.API.Security.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningCenter.API.Security.Authorization.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Authorization process
        var info = (String)context.HttpContext.Items["Info"];
        if (info != "null")
        {
            context.Result = info == "Expired Token" ? 
                new JsonResult(new { message = "ExpiredDD Token" }) { StatusCode = StatusCodes.Status401Unauthorized } : 
                new JsonResult(new { message = "Invalid Token" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null || user.Role.Name == "Inexpert") //If there is no user or the user has role of USER 
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        
    }
}