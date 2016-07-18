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

      Get["/stylist/edit/{id}"] = parameters => {
        Stylist editStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml",editStylist];
      };

      Patch["/stylist/edit/{id}"] = parameters => {
        Stylist editStylist = Stylist.Find(parameters.id);
        editStylist.Update(Request.Form["stylistName"]);
        return View["index.cshtml", Stylist.GetAll()];
      };


      Get["/stylists/{id}/clients"] = paramaters => {
        Dictionary<string, object> clients = new Dictionary<string, object>();
        Stylist stylist = Stylist.Find(paramaters.id);
        List<Client> allClients = Stylist.GetClients(stylist.GetId());
        clients.Add("stylist", stylist);
        clients.Add("clients", allClients);
        return View["stylist.cshtml",clients];
      };


      Get["/clients/edit/{id}"] = parameters => {
        Client editClient = Client.Find(parameters.id);
        return View["client_edit.cshtml",editClient];
      };

      Patch["/client/edit/{id}"] = parameters => {
        Client editClient = Client.Find(parameters.id);
        editClient.Update(Request.Form["clientname"],editClient.GetStylistId());

        Dictionary<string, object> clients = new Dictionary<string, object>();
        Stylist stylist = Stylist.Find(editClient.GetStylistId());
        List<Client> allClients = Stylist.GetClients(editClient.GetStylistId());
        clients.Add("stylist", stylist);
        clients.Add("clients", allClients);
        return View["stylist.cshtml", clients];
      };

      Post["/clients/new/{id}"] =parameters=> {
        int stylist= parameters.id;
        Client newClient = new Client(Request.Form["client-name"], stylist);
        newClient.Save();
        Dictionary <string, object> clients = new Dictionary<string, object>();
        Stylist stylists = Stylist.Find(stylist);
        List<Client> allClients = Stylist.GetClients(stylists.GetId());
        clients.Add("stylist", stylists);
        clients.Add("clients", allClients);

        return View["stylist.cshtml", clients];
      };

      Delete["/clients/delete/{id}"] = parameters=> {

        Client deleteclient = Client.Find(parameters.id);
        deleteclient.Delete();
        int stylistID = deleteclient.GetStylistId();
        Dictionary<string, object> deleteClientDic = new Dictionary<string, object>();
        Stylist stylists = Stylist.Find(stylistID);
        List<Client> allClients = Stylist.GetClients(stylists.GetId());
        deleteClientDic.Add("stylist", stylists);
        deleteClientDic.Add("clients", allClients);
        return View["stylist.cshtml", deleteClientDic];

      };
    }
  }
}
