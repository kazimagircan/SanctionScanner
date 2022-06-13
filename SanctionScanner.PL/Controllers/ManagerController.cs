
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanctionScanner.BAL.Service.Cost;
using SanctionScanner.PL.Authorization;
using System;

namespace SanctionScanner.PL.Controllers
{
    [ServiceFilter(typeof(RoleFilter))]
    [RoleAttribute("Manager")]
    public class ManagerController : Controller
    {
        ICostService _costService;
        IHttpContextAccessor _httpContext;

        public ManagerController(ICostService costService, IHttpContextAccessor httpContext)
        {
            _costService = costService;
            _httpContext = httpContext;
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

                return result;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public JsonResult GetAwaitingCosts()
        {
            try
            {
                var costs = _costService.GetAwaitingCosts(Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId")));
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
                var costs = _costService.GetManagerCosts(Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId")));
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
