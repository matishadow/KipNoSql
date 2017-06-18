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


            var m = new List<ExpandoObject>()
            {
                a
            };

            return PartialView("Books", m);
        }

        public ActionResult EBooks()
        {
            var m = new List<EBook>
            {
                new EBook() {Author = "A", Format = "1", ISBN = "awdawd", SizeInMegaBytes = 3234, id = Guid.NewGuid()}
            };

            return PartialView("EBooks", m);
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