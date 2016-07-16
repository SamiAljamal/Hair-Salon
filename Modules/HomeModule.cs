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
        return View["index.cshtml"];
      };
      Get["/stylists"] =_=>{
        return View["stylists.cshtml",Stylist.GetAll()];
      };
      Post["stylist/new"] =_=> {
        string name = Request.Form["stylist-name"];
        Stylist newStylist = new Stylist(name);
        newStylist.Save();
        List<Stylist> allStylist = Stylist.GetAll();
        return View["stylists.cshtml", allStylist];
      };

      Get["/stylist/{id}"] =parameters=>{
        Stylist stylist = Stylist.Find(parameters.id);
        stylist.GetName();
        return View["stylist.cshtml", stylist];
      };

      Delete["/stylists/clearall"] =_=>{
        Stylist.DeleteAll();
        return View["stylists.cshtml", Stylist.GetAll()];
      };

    }
  }
}
