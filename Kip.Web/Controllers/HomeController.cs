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
            return View();
        }

        public async Task<ActionResult> GetCollection(string type)
        {
            IEnumerable<ExpandoObject> items = await GetCollectionByType(type);

            return PartialView("ItemCollection", items);
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