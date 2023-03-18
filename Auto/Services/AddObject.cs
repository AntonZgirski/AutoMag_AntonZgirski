using Auto.Models;
using Microsoft.Extensions.Configuration;
using Auto.Models;

namespace Auto.Services
{
  public class AddObject
  {
    public void Add(IConfiguration configuration, object obj)
    {
      using (var context = new AutoContext(configuration))
      {        
        context.Add(obj);
        context.SaveChanges();
      }      
    }

    public void Delete(IConfiguration configuration, string models, int id)
    {
      using(var context = new AutoContext(configuration))
      {        
        switch (models)
        {
          case "Auto":
            var auto = context.Autos.Where(p => p.AutoId == id).First();
            context.Autos.Remove(auto);
            break;
          case "Client":
            var client = context.Clients.Where(p => p.ClaentId == id).First();
            context.Clients.Remove(client);
            break;
        }
        context.SaveChanges();
      }
    }
  }
}
