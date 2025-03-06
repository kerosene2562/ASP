using Microsoft.AspNetCore.Mvc;

namespace MvcLab.Controllers
{
    public class LabController : Controller
    {
        public IActionResult ViewInfo()
        {
            var labData = new
            {
                Number = 1,
                Topic = "ASP.NET Core",
                Goal = "get acquainted with the basic principles of .NET, learn about an environmentally friendly development environment and install the necessary components, acquire skills in creating solutions" +
                " and projects of various types, acquire skills in processing requests using middleware",
                Author = "Romanets Oleksandr"
            };

            return View(labData);
        }
    }
}