using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SanctionScanner.BAL.Service.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SanctionScanner.PL.Authorization
{
    public class RoleFilter : IActionFilter
    {
        private readonly IUserRolesService _userRolesService;
        private readonly IHttpContextAccessor _httpContext;
        public RoleFilter(IUserRolesService userRolesService, IHttpContextAccessor httpContext)
        {
            _userRolesService = userRolesService;
            _httpContext = httpContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (HasRoleAttribute(context))
            {
                try
                {
                    var userId = _httpContext.HttpContext.Session.GetInt32("UserId");

                    var arguments = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerTypeInfo.CustomAttributes.FirstOrDefault(fd => fd.AttributeType == typeof(RoleAttribute)).ConstructorArguments;
                    string role = (string)arguments[0].Value;
                    var userRoles = _userRolesService.GetUserRoles(Convert.ToInt32(userId)) as List<string>;

                    if (!userRoles.Contains(role))
                    {
                        //Forbidden 403 Result. Yetkiniz Yoktur..
                        context.Result = new ObjectResult(context.ModelState)
                        {
                            Value = "You are not authorized for this page",
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden
                        };
                        return;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        public bool HasRoleAttribute(FilterContext context)
        {
            return ((ControllerActionDescriptor)context.ActionDescriptor).ControllerTypeInfo.CustomAttributes.Any(filterDescriptors => filterDescriptors.AttributeType == typeof(RoleAttribute));
        }


    }
}
