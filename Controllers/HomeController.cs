﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using floral_fusion.Models;

using System.Xml;
using Newtonsoft.Json;
namespace floral_fusion.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public Object UserExist = new
    {
        exist = false
    };

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var user = Request?.Cookies["User"];
        int CartCount = 0;
        if (user != null)
        {
            var cartItem = new XmlDocument();
            cartItem.Load("./database/cartitem.xml");
            foreach (XmlNode Item in cartItem?.SelectNodes("CartData/Item"))
            {
                Console.WriteLine();
                if (Item.SelectSingleNode("user")?.InnerText == user)
                {
                    CartCount++;
                }
            }
        }
        var xmlDoc = new XmlDocument();
        xmlDoc.Load("./database/products.xml");
        var products = new List<Product>();
        foreach (XmlNode node in xmlDoc?.SelectNodes("/products/product"))
        {
            if (node != null)
            {
                var product = new Product
                {
                    Id = Convert.ToInt32(node?.SelectSingleNode("id")?.InnerText),
                    Title = node?.SelectSingleNode("title")?.InnerText,
                    Image = node?.SelectSingleNode("image")?.InnerText,
                    Price = Convert.ToDecimal(node?.SelectSingleNode("price")?.InnerText),
                    Description = node?.SelectSingleNode("description")?.InnerText,
                    // Tags = node?.SelectSingleNode("Tags")?.InnerText,
                    // CreatedAt = node?.SelectSingleNode("createdAt")?.InnerText,
                };
                products.Add(product);
            }
        }
        // if(user != null){
        //     var x = JsonConvert.DeserializeObject(user);
        // }
        ViewData["CartCount"] = CartCount;
        ViewData["User"] = user;
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}