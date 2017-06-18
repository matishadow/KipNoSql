using System;
using System.Collections.Generic;
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
                new Book() {Author = "A", ISBN = "b", PageCount = 4, Title = "c"}
            };
            return View(new BaseCollections(){Books = m});
        }

        public ActionResult Books()
        {
            var m = new List<Book>()
            {
                new Book() {Author = "A", ISBN = "b", PageCount = 4, Title = "c"}
            };

            return PartialView("Books", m);
        }

        public ActionResult EBooks()
        {
            var m = new List<EBook>
            {
                new EBook() {Author = "A", Format = "1", ISBN = "awdawd", SizeInMegaBytes = 3234}
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