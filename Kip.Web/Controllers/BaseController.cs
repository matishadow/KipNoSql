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
