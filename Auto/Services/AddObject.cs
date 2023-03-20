using Auto.Models;
using Microsoft.Extensions.Configuration;
using Auto.Models;

namespace Auto.Services
{
  public class AddObject
  {
    public AutoContext Context { get; set; }
    public AddObject(IConfiguration configuration)
    {
      Context = new AutoContext(configuration);
    }
    public void Add(IConfiguration configuration, object obj)
    {
      Context.Add(obj);
      Context.SaveChanges();
    }

    public void Delete(IConfiguration configuration, string models, int id)
    {
      switch (models)
      {
        case "Auto":
          var auto = Context.Autos.Where(p => p.AutoId == id).First();
          Context.Autos.Remove(auto);
          break;
        case "Client":
          var client = Context.Clients.Where(p => p.ClaentId == id).First();
          Context.Clients.Remove(client);
          break;
      }
      Context.SaveChanges();
    }
  }
}
