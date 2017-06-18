using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kip.Core;
using Kip.Models;
using Kip.Models.Base;
using Microsoft.Azure.Documents.Client;

namespace Kip.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var m = new List<Book>()
            {
                new Book() {Author = "A", ISBN = "b", PageCount = 4, Title = "c", id = Guid.NewGuid()}
            };
            return View(new BaseCollections(){Books = m});
        }

        private static DocumentDbCredentials CreateDocumentDbCredentials()
        {
            string databaseId = ConfigurationManager.AppSettings["database"];
            string collectionId = ConfigurationManager.AppSettings["collection"];
            string endpoint = ConfigurationManager.AppSettings["endpoint"];
            string authKey = ConfigurationManager.AppSettings["authKey"];

            return new DocumentDbCredentials(endpoint, authKey, databaseId, collectionId);
        }


        public async Task<ActionResult> Books()
        {
            var items = await GetCollectionByType("Book");

            return PartialView("ItemCollection", items);
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