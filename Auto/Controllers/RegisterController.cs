using Auto.Models;
using Auto.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Auto.Controllers
{
  public class RegisterController : Controller
  {
    private readonly ILogger<RegisterController> _logger;
    private readonly AddObject _addobj;    

    public RegisterController(ILogger<RegisterController> logger, AddObject addObject)
    {
      _logger = logger;
      _addobj = addObject;
    }
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Authenfic([FromForm] string login, [FromForm] string password)
    {
      var user = _addobj.GetUserFirst(login);
      if (user == null)
      {
        ViewBag.Error = "Пользователь не найден!";
        return View("Index");
      }
      if (user.Password == password)
      {        
        switch (user.Role.RoleName)
        {
          case "Client":
            _addobj.WriteUserId(user.UserId);
            return RedirectToAction("Auto", "Users");
          case "Employee":
            _addobj.WriteUserId(user.UserId);
            return RedirectToAction("Applic", "Employee");
          default:
            ViewBag.ErrorAut = "Неверный логин или пароль";
            return View("Index");
        }
      }
      else
      {
        ViewBag.Error = "Неверный пароль!";
        return View("Index");
      }

    }
  }
}
