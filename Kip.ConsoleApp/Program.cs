using System;
using System.Configuration;
using Kip.Core;
using Kip.Models;
using Kip.Models.Base;
using Microsoft.Azure.Documents.Client;

namespace Kip.ConsoleApp
{
    class Program
    {
        private static DocumentDbCredentials CreateDocumentDbCredentials()
        {
            string databaseId = ConfigurationManager.AppSettings["database"];
            string collectionId = ConfigurationManager.AppSettings["collection"];
            string endpoint = ConfigurationManager.AppSettings["endpoint"];
            string authKey = ConfigurationManager.AppSettings["authKey"];

            return new DocumentDbCredentials(endpoint, authKey, databaseId, collectionId);
        }

        static void Main(string[] args)
        {
          

            Console.Read();
        }

    }
}
