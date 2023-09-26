using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Welcome(string name="Zohaib", int numTimes = 1)
    {
        ViewData["Message"] = "Hi Hello " + name + " Check my message!";
        ViewData["NumTimes"] = numTimes;
        return View();
    }
}