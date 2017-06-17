using Kip.Models;
using Microsoft.Azure.Documents;

namespace Kip.Core
{
    public class DocumentDbBase
    {
        protected readonly IDocumentClient DocumentClient;
        protected readonly DocumentDbCredentials DocumentDbCredentials;

        public DocumentDbBase(DocumentDbCredentials documentDbCredentials, IDocumentClient documentClient)
        {
            DocumentDbCredentials = documentDbCredentials;
            DocumentClient = documentClient;
        }
    }
}