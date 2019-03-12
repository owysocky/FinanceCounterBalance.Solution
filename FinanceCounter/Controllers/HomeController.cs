using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/home/aboutus")]
      public ActionResult AboutUs()
      {
        return View();
      }

      [HttpGet("/home/contact")]
      public ActionResult Contact()
      {
        return View();
      }

    }
}
