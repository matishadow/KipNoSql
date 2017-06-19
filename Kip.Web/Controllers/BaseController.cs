using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kip.Core;
using Kip.Interfaces;
using Kip.Models;
using Kip.Models.Base;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Kip.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IDocumentClient DocumentClient;

        protected readonly IDocumentDbItemCreator<ExpandoObject> ItemCreator;
        protected readonly IDocumentDbItemDeleter ItemDeleter;
        protected readonly IDocumentDbItemGetter<ExpandoObject> ItemGetter;
        protected readonly IDocumentDbItemUpdater<ExpandoObject> ItemUpdater;

        protected BaseController()
        {
            DocumentDbCredentials documentDbCredentials = CreateDocumentDbCredentials();

            DocumentClient = new DocumentClient(new Uri(documentDbCredentials.Endpoint), documentDbCredentials.AuthKey);

            ItemCreator = new DocumentDbItemCreator<ExpandoObject>(documentDbCredentials, DocumentClient);
            ItemDeleter = new DocumentDbItemDeleter(documentDbCredentials, DocumentClient);
            ItemGetter = new DocumentDbItemGetter<ExpandoObject>(documentDbCredentials, DocumentClient);
            ItemUpdater = new DocumentDbItemUpdater<ExpandoObject>(documentDbCredentials, DocumentClient);
        }

        protected async Task<IEnumerable<ExpandoObject>> GetCollectionByType(string type)
        {
            IEnumerable<ExpandoObject> items = await ItemGetter.GetItemsAsync();

            items = items.Where(item => ((dynamic)item).Type == type).ToList();
            return items;
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            dynamic item = await ItemGetter.GetItemAsync(id);

            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FormCollection collection)
        {
            if (!collection.HasKeys())
                return RedirectToAction("Edit");


            var expandoCollection = new ExpandoObject() as IDictionary<string, object>;
            foreach (string key in collection.AllKeys)
            {
                if (key == null)
                    continue;

                expandoCollection.Add(new KeyValuePair<string, object>(key, collection[key]));
            }
            dynamic expandoObject = (ExpandoObject)expandoCollection;

            await ItemUpdater.UpdateItemAsync(new Guid(expandoObject.id), expandoObject);

            return RedirectToAction("Index");
        }

        protected async Task<ActionResult> Create(FormCollection collection, string controllerName)
        {
            if (!collection.HasKeys())
                return RedirectToAction("Create");

            var expandoCollection = new ExpandoObject() as IDictionary<string, object>;
            foreach (string key in collection.AllKeys)
            {
                if (key != null)
                    expandoCollection.Add(new KeyValuePair<string, object>(key, collection[key]));
            }

            dynamic expandoObject = (ExpandoObject)expandoCollection;
            expandoObject.id = Guid.NewGuid();
            expandoObject.Type = controllerName;

            await ItemCreator.CreateItemAsync(expandoObject);
            return RedirectToAction("Index");
        }

        private static DocumentDbCredentials CreateDocumentDbCredentials()
        {
            string databaseId = ConfigurationManager.AppSettings["database"];
            string collectionId = ConfigurationManager.AppSettings["collection"];
            string endpoint = ConfigurationManager.AppSettings["endpoint"];
            string authKey = ConfigurationManager.AppSettings["authKey"];

            return new DocumentDbCredentials(endpoint, authKey, databaseId, collectionId);
        }
    }
}
