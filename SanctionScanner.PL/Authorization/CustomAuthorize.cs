using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SanctionScanner.BAL.Service.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanctionScanner.PL.Authorization
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        //private readonly string[] allowedroles;
        //private IUserRolesService urs;


        //public CustomAuthorize(RequestDelegate next, params string[] roles)
        //{
        //    this.allowedroles = roles;
        //    this.urs = new UserRolesService();
        //    _next = next;
        //}
        public CustomAuthorizeAttribute(string claimType, string claimValue) : base(typeof(CustomAuthorizeFilter))
        {

            Arguments = new object[] { new Claim(claimType, claimValue) };
            //this.urs = new UserRolesService();
        }

        public class CustomAuthorizeFilter : IAuthorizationFilter
        {
            readonly Claim _claim;
            IHttpContextAccessor _httpContext;

            public CustomAuthorizeFilter(Claim claim, IHttpContextAccessor httpContext)
            {
                _claim = claim;
                _httpContext = httpContext;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
              
                var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
                if (!hasClaim)
                {
                    context.Result = new ForbidResult();
                }
            }

        }
        //public async Task Invoke(HttpContext httpContext)
        //{
        //    //bool authorize = false;
        //    foreach (var role in allowedroles)
        //    {
        //        var userId = httpContext.Session.GetString("UserId");

        //        var roles = urs.GetUserRoles(Convert.ToInt32(userId)) as List<string>;
        //        if (roles.Contains(role))
        //        {
        //            await _next(httpContext);

        //        }

        //    }

        //}
    }
}
