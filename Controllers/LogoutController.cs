using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class LogoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Confirm()
    {
        Response.Cookies.Delete("User");
        return Redirect("/login");
    }
}