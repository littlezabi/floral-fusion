using Microsoft.AspNetCore.Mvc;

[ViewComponent]
public class HeadersViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name)
    {
        return View("Default", name);
    }
}
