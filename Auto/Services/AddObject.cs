using Auto.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;

namespace Auto.Services
{
  public class AddObject
  {
    private readonly AutoContext Context;
    public AddObject(AutoContext context)
    {
      Context = context;
    }

    public void WriteUserId(int id)
    {

      using (FileStream fs = new FileStream("userid.json", FileMode.Create, FileAccess.Write, FileShare.None))
      {
        JsonSerializer.SerializeAsync(fs, id);        
      }
    }

    public int GetUserId()
    {
      using (FileStream fs = new FileStream("userid.json", FileMode.Open, FileAccess.Read, FileShare.None))
      {
        var per = JsonSerializer.Deserialize<int>(fs); 
        return per;
      }
    }

    public List<Auto.Models.Auto> GetAuto()
    {
      return Context.Autos.Where(p => p.Status == "Add").ToList();
    }

    public Auto.Models.Auto GetAuto(int id)
    {
      var auto = Context.Autos.Where(p => p.AutoId == id).FirstOrDefault();
      return auto;
    }

    public List<Client> GetClients()
    {
      return Context.Clients.Include(p => p.Magazines).ToList();
    }

    public void Add(object obj)
    {
      Context.Add(obj);
      Context.SaveChanges();
    }

    public void Delete(string models, int id)
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

    public User GetUserFirst(string login)
    {
      var user = Context.Users.Where(p => p.Login == login).FirstOrDefault();
      var role = Context.Roles.FirstOrDefault(p => p.RoleId == user.RoleId);
      user.Role = role;
      return user;
    }

    public List<AppView> GetApplic()
    {
      var listApp = (from a in Context.Applications
                    join c in Context.Autos on a.AutoId equals c.AutoId
                    join d in Context.Users on a.UserId equals d.UserId
                    select new AppView { ApplicId = a.ApplicId, Name = d.Login, Model = c.AutoModel, Status = a.Status }).ToList();
      return listApp;
    }

    public void ConfirmApp(int id)
    {
      var app = Context.Applications.Where(p => p.ApplicId == id).FirstOrDefault();
      app.Status = "Confirm";
      var auto = Context.Autos.Where(p => p.AutoId == app.AutoId).FirstOrDefault();
      auto.Status = "Sales";
      Context.SaveChanges();
    }

    public void RejectApp(int id)
    {
      var app = Context.Applications.Where(p => p.ApplicId == id).FirstOrDefault();
      app.Status = "Reject";
      var auto = Context.Autos.Where(p => p.AutoId == app.AutoId).FirstOrDefault();
      auto.Status = "Add";
      Context.SaveChanges();
    }

  }
}
