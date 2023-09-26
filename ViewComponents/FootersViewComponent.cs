using Microsoft.AspNetCore.Mvc;

[ViewComponent]
public class FootersViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("Default");
    }
}
