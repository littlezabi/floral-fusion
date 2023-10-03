using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
public class LoginController : Controller
{
    public IActionResult Index()
    {
        var redirectTo = Request.Query["redirect"];
        ViewData["pageView"] = redirectTo;
        return View();
    }
    public ActionResult Submit(RegistrationModel model)
    {
        var redirectTo = Request.Query["redirect"];
        if(redirectTo == "") redirectTo = "Home";
        ViewData["pageView"] = redirectTo;
        Console.WriteLine(redirectTo);
        XElement xml = XElement.Load("./database/users.xml");
        TempData["variant"] = "success";
        var user = xml.Elements("User")
                      .FirstOrDefault(u =>
                         u.Element("email")?.Value == model.Email &&
                         u.Element("password")?.Value == model.Password);

        if (user != null)
        {
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                HttpOnly = false,
                Expires = DateTime.Now.AddHours(24 * 30 * 12),
            };
            string email = user?.Element("email")?.Value ?? "";
            Response.Cookies.Append("User", email, cookieOptions);
            TempData["message"] = "Successfully Logged!";
            return Redirect($"/{redirectTo}");
        }
        else
        {
            TempData["variant"] = "danger";
            TempData["message"] = "User email or password is incorrect!";
        }
        return Redirect($"/login?redirect={redirectTo}");
    }
}