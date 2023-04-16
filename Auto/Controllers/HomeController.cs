using Auto.Models;
using Auto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace Auto.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly AddObject _addobj;

    public HomeController(ILogger<HomeController> logger, AddObject addObject)
    {
      _logger = logger;
      _addobj = addObject;
    }

    
    public IActionResult Index()
    {
      return View();
    }
    
    public IActionResult Client()
    {      
      return View(_addobj.GetClients());
    }
   
    public IActionResult Auto()
    {            
      return View(_addobj.GetAuto());
    }    

    [HttpGet]
    public IActionResult AddAuto()
    {
      return View();
    }

    [HttpPost]
    public IActionResult CreateAuto([FromForm] string model, [FromForm] int year, [FromForm] decimal price)
    {
      var auto = new Auto.Models.Auto { AutoModel = model, Year = year, Price = price , Status = "Add"};
      _addobj.Add(auto);
      return RedirectToAction("Auto");
    }

    public IActionResult EditAuto(int id)
    {
      return RedirectToAction("Auto");
    }

    public IActionResult DeleteAuto(int id, string name)
    {
      _addobj.Delete(name, id);
      return RedirectToAction("Auto");
    }
    
    [HttpGet]
    public IActionResult AddClient()
    {
      return View();
    }

    [HttpPost]
    public IActionResult CreateClient([FromForm] string fname, [FromForm] string sname)
    {
      var client = new Client { ClientName = fname, ClientSname = sname };
      _addobj.Add(client);
      return RedirectToAction("Client");
    }

    public IActionResult DeleteClient(int id, string name)
    {
      _addobj.Delete(name, id);
      return RedirectToAction("Client");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}