using System.Xml;
using Microsoft.AspNetCore.Mvc;

[ViewComponent]
public class AdminHeadersViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name)
    {
        var user = Request?.Cookies["User"];
        int CartCount = 0;
        if (user != null)
        {
            var cartItem = new XmlDocument();
            cartItem.Load("./database/cartitem.xml");
            var nodes = cartItem?.SelectNodes("CartData/Item") ?? null;
            if (nodes != null)
            {
                foreach (XmlNode Item in nodes)
                {
                    Console.WriteLine();
                    if (Item.SelectSingleNode("user")?.InnerText == user)
                    {
                        CartCount++;
                    }
                }
            }
        }
        return View("Default", user +"|"+ CartCount.ToString());
    }
}
