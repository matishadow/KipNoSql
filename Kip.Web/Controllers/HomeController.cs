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

        public async Task<ActionResult> Delete(Guid id)
        {
            await ItemDeleter.DeleteItemAsync(id);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            if (!collection.HasKeys())
                return RedirectToAction("Create");

            var expandoCollection = new ExpandoObject() as IDictionary<string, object>;
            foreach (string key in collection.AllKeys)
               expandoCollection.Add(new KeyValuePair<string, object>(key, collection[key]));

            dynamic expandoObject = (ExpandoObject) expandoCollection;
            expandoObject.id = Guid.NewGuid();

            await ItemCreator.CreateItemAsync(expandoObject);
            return RedirectToAction("Index");
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