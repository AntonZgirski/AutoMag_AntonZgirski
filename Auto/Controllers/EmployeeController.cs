using Auto.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auto.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly ILogger<EmployeeController> _logger;
    private readonly AddObject _addobj;
    private int _userid;

    public EmployeeController(ILogger<EmployeeController> logger, AddObject addObject)
    {
      _logger = logger;
      _addobj = addObject;
    }

    public IActionResult Applic()
    {      
      return View(_addobj.GetApplic());
    }

    public IActionResult ConfirmApp(int id)
    {
      _addobj.ConfirmApp(id);
      return RedirectToAction("Applic");
    }

    public IActionResult RejectApp(int id)
    {
      _addobj.RejectApp(id);
      return RedirectToAction("Applic");
    }

  }
}
