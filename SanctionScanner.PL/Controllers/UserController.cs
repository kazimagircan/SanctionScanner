using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanctionScanner.BAL.Service.Cost;
using SanctionScanner.PL.Authorization;
using System;
using System.Threading.Tasks;

namespace SanctionScanner.PL.Controllers
{
    [ServiceFilter(typeof(RoleFilter))]
    [RoleAttribute("User")]
    public class UserController : Controller
    {
        ICostService _costService;
        IHttpContextAccessor _httpContext;
        public UserController(ICostService costService, IHttpContextAccessor httpContext)
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


        public IActionResult CreateCost()
        {
            return View();
        }

        [HttpPost]
        public bool CreateCost(CostDto data)
        {
            try
            {
                data.UserId = Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId"));
                var result = _costService.AddCost(data);

                return result;

            }
            catch (Exception)
            {

                return false;
            }
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

        public JsonResult GetRejectedCost()
        {
            try
            {
                var costs = _costService.GetRejectedCosts(Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId")));
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
                var costs = _costService.GetCosts(Convert.ToInt32(_httpContext.HttpContext.Session.GetInt32("UserId")));
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
