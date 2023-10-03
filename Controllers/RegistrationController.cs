using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Xml.Linq;

using Newtonsoft.Json;
namespace floral_fusion.Controllers;

public class RegistrationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public ActionResult ProcessForm(RegistrationModel model)
    {
        TempData["variant"] = "danger";
        if (model.FirstName == "") TempData["message"] = "Please Enter Your firstname.";
        if (model.LastName == "") TempData["message"] = "Please Enter Your lastname.";
        else if (model.Email == "") TempData["message"] = "Please Enter Your Email adress.";
        else if (model.Password == "") TempData["message"] = "Please Enter Your Password.";
        else if (model.RePassword != model.Password) TempData["message"] = "Passwords Not Match Please Check Passwords.";
        else if (model.Password?.Length < 8) TempData["message"] = "Please Enter Minimum 8 Characters Password.";
        else
        {
            string filePath = "./database/users.xml";
            XDocument xmlDoc = XDocument.Load(filePath);
            var k = xmlDoc.Element("UsersList")?.Elements("User");
            var checkExistence = k?.FirstOrDefault(u => u.Element("email")?.Value == model.Email);
            if (checkExistence != null)
            {
                TempData["message"] = "User email is already exist please login with this email address or type new!";
            }
            else
            {
                XElement user = new("User");
                XElement FirstName = new("firstname", model.FirstName);
                XElement LastName = new("lastname", model.LastName);
                XElement Email = new("email", model.Email);
                XElement Password = new("password", model.Password);
                XElement CreatedAt = new("createdAt", DateAndTime.DateString);
                XElement asAdmin = new("asAdmin", (model.asAdmin != null) ? 1 : 0);
                user.Add(FirstName);
                user.Add(LastName);
                user.Add(Email);
                user.Add(Password);
                user.Add(CreatedAt);
                user.Add(asAdmin);
                xmlDoc?.Element("UsersList")?.Add(user);
                xmlDoc?.Save(filePath);
                var UserObject = new
                {
                    firstname = FirstName,
                    lastname = LastName,
                    email = Email,
                    createdAt = CreatedAt,
                };
                var cookieOptions = new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    Expires = DateTime.Now.AddHours(24 * 30 * 12),
                };
                Response.Cookies.Append("User", JsonConvert.SerializeObject(UserObject), cookieOptions);
                TempData["variant"] = "success";
                TempData["message"] = "User added successfully!";
                return Redirect("/");
            }
        }
        return RedirectToAction("");
    }
}