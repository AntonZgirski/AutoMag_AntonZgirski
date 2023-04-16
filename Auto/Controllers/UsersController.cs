using Auto.Models;
using Auto.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auto.Controllers
{
  public class UsersController : Controller
  {
    private readonly ILogger<UsersController> _logger;
    private readonly AddObject _addobj;
    private int _userid;

    public UsersController(ILogger<UsersController> logger, AddObject addObject)
    {
      _logger = logger;
      _addobj = addObject;
    }

    public IActionResult Auto()
    {            
      return View(_addobj.GetAuto());
    }
    
    public IActionResult CreateApp(int id)
    {
      var auto = _addobj.GetAuto(id);
      auto.Status = "Booked";     
      var app = new Application { AutoId = auto.AutoId, UserId = _addobj.GetUserId(), Status = "Booked" };
      _addobj.Add(app);
      return RedirectToAction("Auto");      
    }
  }
}
