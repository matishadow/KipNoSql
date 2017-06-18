using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        protected readonly IDocumentDbItemCreator<dynamic> ItemCreator;
        protected readonly IDocumentDbItemDeleter ItemDeleter;
        protected readonly IDocumentDbItemGetter<dynamic> ItemGetter;
        protected readonly IDocumentDbItemUpdater<dynamic> ItemUpdater;

        protected BaseController()
        {
            DocumentDbCredentials documentDbCredentials = CreateDocumentDbCredentials();

            DocumentClient = new DocumentClient(new Uri(documentDbCredentials.Endpoint), documentDbCredentials.AuthKey);

            ItemCreator = new DocumentDbItemCreator<dynamic>(documentDbCredentials, DocumentClient);
            ItemDeleter = new DocumentDbItemDeleter(documentDbCredentials, DocumentClient);
            ItemGetter = new DocumentDbItemGetter<dynamic>(documentDbCredentials, DocumentClient);
            ItemUpdater = new DocumentDbItemUpdater<dynamic>(documentDbCredentials, DocumentClient);
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
