using Nancy;
using System.Collections.Generic;
using System;
using Library.Objects;

namespace Library
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {

      Get["/"]=_=> {
        return View["index.cshtml"];
      };

    //   Get["/patrons/patroninfo"] = _ => {
    //     Patron patron = new Patron (Request.Form["patron-id"]);
    //     Dictionary<string, object> model = new Dictionary<string, object>();
    //     Stylist stylist = Stylist.Find(paramaters.id);
    //     List<Copy> book_checkouts = Patron.GetCheckOuts();
    //     List<Copy> copy_checkouts = Patron.GetCheckOuts();
    //     List<Book> history = Patron.GetHistory();
    //     model.Add("patron", patron);
    //     model.Add("checkouts", allClients);
    //     model.Add("history", allClients);
    //     return View ["/patron.cshtml", model];
    //   };
    //
    }
  }
}
