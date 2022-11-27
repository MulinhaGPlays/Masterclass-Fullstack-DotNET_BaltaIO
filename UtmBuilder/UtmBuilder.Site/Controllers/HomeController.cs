using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UtmBuilder.Site.Models;

namespace UtmBuilder.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult UtmGenerator(UtmModel model)
        {
            if (ModelState.IsValid)
            {
                try 
	            {
                    ViewBag.StyleType = "alert-success";
                    model.Url = ((Utm)model).ToString();
                }
	            catch (Exception ex)
	            {
                    ViewBag.StyleType = "alert-danger";
                    model.Url = ex.Message;
	            }   
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}