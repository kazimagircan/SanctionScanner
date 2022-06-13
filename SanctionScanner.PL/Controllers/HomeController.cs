using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SanctionScanner.BAL.Service.User;
using SanctionScanner.BAL.Service.UserRoles;
using SanctionScanner.PL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanctionScanner.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IUserRolesService _userRolesService;
        private readonly IHttpContextAccessor _httpContext;
        public HomeController(ILogger<HomeController> logger, IUserService userService, IUserRolesService userRolesService, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _userService = userService;
            _userRolesService = userRolesService;
            _httpContext = httpContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var _user = _userService.CheckUser(model.Email, model.Password);
                if (_user != null)
                {
                    var roles = _userRolesService.GetUserRoles(_user.Id) as List<string>;

                    if (_user.UserRoles.Any(r=>r.Name== "Manager"))
                    {
                        _httpContext.HttpContext.Session.SetInt32("UserId", _user.Id);
                        _httpContext.HttpContext.Session.SetInt32("ManagerId", _user.ManagerId);
                        return RedirectToAction("Index", "Manager");
                    }
                    else if (_user.UserRoles.Any(r => r.Name == "Accountant"))
                    {
                        _httpContext.HttpContext.Session.SetInt32("UserId", _user.Id);
                        _httpContext.HttpContext.Session.SetInt32("ManagerId", _user.ManagerId);
                        return RedirectToAction("Index", "Accountant");

                    }
                    else if (_user.UserRoles.Any(r => r.Name == "User"))
                    {
                        _httpContext.HttpContext.Session.SetInt32("UserId", _user.Id);
                        _httpContext.HttpContext.Session.SetInt32("ManagerId", _user.ManagerId);
                        return RedirectToAction("Index", "User");

                    }

                }
                else
                {
                    ModelState.AddModelError("NotUser", "Böyle bir kullanıcı bulunmamaktadır.");
                    ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
                }
            }
            return View(model);
        }

    }
}
