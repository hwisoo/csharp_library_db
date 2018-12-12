using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
         [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
