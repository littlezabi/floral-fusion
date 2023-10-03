using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Xml;
using System.Xml.Linq;
namespace MvcMovie.Controllers;

public class CheckoutController : Controller
{
    private readonly string filePath = "./database/cartitem.xml";
    public ActionResult Index()
    {
        TempData["message"] = "";
        var user = Request?.Cookies["User"];
        if (user != null)
        {
            XElement xml = XElement.Load("./database/users.xml");
            var getUser = xml.Elements("User")
                          .FirstOrDefault(u =>
                             u.Element("email")?.Value == user);
            var user_ = new
            {
                firstname = getUser?.Element("firstname")?.Value,
                lastname = getUser?.Element("lastname")?.Value,
                email = user
            };
            ViewData["cartUser"] = user_;
            string filePath = "./database/cartitem.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var cartNodes = xmlDoc?.SelectNodes("/CartData/Item") ?? null;
            XElement productXml = XElement.Load("./database/products.xml");
            var cartItems = new List<object>();
            int count = 0;
            decimal getPrice = 0;
            if (cartNodes != null)
            {
                foreach (XmlNode node in cartNodes)
                {
                    if (node?.SelectSingleNode("user")?.InnerText == user)
                    {
                        var pid = node?.SelectSingleNode("product_id")?.InnerText ?? null;
                        var getProduct = productXml.Elements("product")
                                      .FirstOrDefault(u =>
                                         u.Element("id")?.Value == pid);
                        count++;
                        var p = getProduct?.Element("price")?.Value;
                        if (p != null) getPrice += Convert.ToDecimal(p);
                        var item = new
                        {
                            count = count,
                            id = getProduct?.Element("id")?.Value,
                            title = getProduct?.Element("title")?.Value,
                            image = getProduct?.Element("image")?.Value,
                            description = getProduct?.Element("description")?.Value,
                            price = getProduct?.Element("price")?.Value,
                        };
                        cartItems.Add(item);
                    }
                }
            }
            ViewData["cartPrice"] = getPrice;
            return View(cartItems);
        }
        else
        {
            return Redirect("/login");
        }
    }
}
