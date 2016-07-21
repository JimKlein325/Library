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

      Post["/patrons/patroninfo"] = parameters => {
        int patronId = Request.Form["patron-id"];
        Patron patron = Patron.Find(patronId);

        Dictionary<string, object> model = new Dictionary<string, object>();
        // List<Copy> book_checkouts = Patron.GetCheckOuts();
        List<Copy> patron_checkouts = Copy.GetCheckedOutCopies(patron);
        //List<Book> history = Patron.GetHistory();
        Console.WriteLine("# checkouts:" + patron_checkouts.Count);
        model.Add("patron", patron);
        model.Add("checkouts", patron_checkouts);
        //model.Add("history", allClients);
        return View ["/patron.cshtml", model];
      };

    }
  }
}
