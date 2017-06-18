using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kip.Models;
using Kip.Models.Base;

namespace Kip.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var m = new List<Book>()
            {
                new Book() {Author = "A", ISBN = "b", PageCount = 4, Title = "c", id = Guid.NewGuid()}
            };
            return View(new BaseCollections(){Books = m});
        }

        public ActionResult Books()
        {
            dynamic a = new ExpandoObject();
            a.Author = "A";
            a.ISBN = "B";
            a.PageCount = 4;
            a.Title = "C";
            a.id = Guid.NewGuid();

            dynamic c = new ExpandoObject();
            c.Author = "A";
            c.ISBN = "B";
            c.PageCount = 4;
            c.Title = "C";
            c.id = Guid.NewGuid();

            dynamic d = new ExpandoObject();
            d.Author = "A";
            d.ISBN = "B";
            d.PageCount = 4;
            d.Title = "C";
            d.id = Guid.NewGuid();
            d.FUCKaHISSHIT = "a";

            dynamic b = new ExpandoObject();
            b.Title = "C";
            b.id = Guid.NewGuid();


            var m = new List<ExpandoObject>()
            {
                a, b, c, d
            };

            return PartialView("ItemCollection", m);
        }

        public ActionResult EBooks()
        {
            dynamic a = new ExpandoObject();
            a.Author = "A";
            a.Format = "pdf";
            a.ISBN = "AFawfaw";
            a.SizeInMegaBytes = 2324;
            a.id = Guid.NewGuid();

            var m = new List<ExpandoObject>
            {
                a
            };

            return PartialView("ItemCollection", m);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult PresentationLink()
        {
            return View();
        }
    }
}