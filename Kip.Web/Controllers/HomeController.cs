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