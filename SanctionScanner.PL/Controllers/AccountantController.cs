using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SanctionScanner.BAL.Service.Cost;
using SanctionScanner.BAL.Service.User;
using SanctionScanner.Infrastructure.Email.Service;
using SanctionScanner.PL.Authorization;
using System;

namespace SanctionScanner.PL.Controllers
{
    [ServiceFilter(typeof(RoleFilter))]
    [RoleAttribute("Accountant")]
    public class AccountantController : Controller
    {
        ICostService _costService;
        IUserService _userService;
        IHttpContextAccessor _httpContext;
        private readonly IEmailSender _emailSender;

        public AccountantController(ICostService costService, IHttpContextAccessor httpContext, IEmailSender emailSender, IUserService userService)
        {
            _costService = costService;
            _httpContext = httpContext;
            _emailSender = emailSender;
            _userService= userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cost()
        {
            return View();
        }

        [HttpPost]
        public bool EditCost(CostDto data)
        {
            try
            {
                var result = _costService.UpdateCost(data);
                var user = _userService.GetUserById(data.UserId);
                var message = new SanctionScanner.Infrastructure.Email.Model.Message(new string[] { user.Email }, "Payment Service", "Your payment is sent.", null);

                _emailSender.SendEmail(message);
                return result;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public JsonResult GetWaitingPaymentCosts()
        {
            try
            {
                var costs = _costService.GetWaitingPaymentCosts(Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId")));
                return Json(costs);

            }
            catch (Exception)
            {

                return null;
            }
        }

        public JsonResult GetCosts()
        {
            try
            {
                var costs = _costService.GetAccountantCosts(Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId")));
                return Json(costs);

            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPost]
        public void Logout()
        {
            try
            {
                _httpContext.HttpContext.Session.SetInt32("UserId", -1);
                RedirectToAction("Login", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
