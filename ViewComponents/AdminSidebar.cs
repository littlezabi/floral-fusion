using System.Xml;
using Microsoft.AspNetCore.Mvc;

[ViewComponent]
public class AdminSidebarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("Default");
    }
}
