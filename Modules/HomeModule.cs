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
      Post["stylist"] =_=> {
        string name = Request.Form["stylist-name"];
        Stylist newStylist = new Stylist(name);
        newStylist.Save();
        return View["index.cshtml", Stylist.GetAll()];
      };
    }
  }
}
