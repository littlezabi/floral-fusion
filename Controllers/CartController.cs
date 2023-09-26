using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Xml.Linq;
namespace MvcMovie.Controllers;

public class CartController : Controller
{
    public string Add(int ItemId)
    {
        var user = Request?.Cookies["User"];
        if (user != null)
        {
            string filePath = "./database/cartitem.xml";
            XDocument xmlDoc = XDocument.Load(filePath);
            var k = xmlDoc.Element("CartData")?.Elements("Item");
            var checkExistence = k?.FirstOrDefault(u => u.Element("user")?.Value == user && u.Element("product_id")?.Value == ItemId.ToString());
            if (checkExistence != null)
            {
                return "ProductAlreadyExist";
            }
            else
            {
                XElement Item = new("Item");
                XElement userEmail = new("user", user);
                XElement pid = new("product_id", ItemId);
                XElement CreatedAt = new("createdAt", DateAndTime.DateString);
                Item.Add(userEmail);
                Item.Add(pid);
                Item.Add(CreatedAt);
                xmlDoc?.Element("CartData")?.Add(Item);
                xmlDoc?.Save(filePath);
                return "Success";
            }
        }
        else
        {
            return "UserNotLogged";
        }
    }
    public ActionResult Index()
    {
        return View();
    }
}
