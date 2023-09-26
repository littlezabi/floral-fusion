using Microsoft.AspNetCore.Mvc;

[ViewComponent]
public class HelloWorldViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name)
    {
        return View("Default", name);
    }
}
