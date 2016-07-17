using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Salon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"]=_=>{
        List<Stylist> allStylist = Stylist.GetAll();
        return View["index.cshtml", allStylist];
      };
      Post["/"] = _ => {
        Stylist stylist = new Stylist(Request.Form["stylist-name"]);
        stylist.Save();
        List<Stylist> allStylist = Stylist.GetAll();
        return View["index.cshtml", allStylist];
      };

      Delete["/stylist/delete/{id}"] = paramaters => {
        Stylist Selectedstylist = Stylist.Find(paramaters.id);
        Selectedstylist.Delete();
        return View["index.cshtml", Stylist.GetAll()];
      };

      Get["stylists/{id}/clients"] = paramaters => {
        Stylist stylist = Stylist.Find(paramaters.id);
        return View["stylist.cshtml",stylist.GetClients()];
      };






    }
  }
}
