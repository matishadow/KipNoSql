using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using LibProject.Core;
using LibProject.Interfaces;
using LibProject.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.SystemFunctions;
using Moq;
using NUnit.Framework;

namespace LibProject.Tests
{
    [TestFixture(typeof(Book))]
    [TestFixture(typeof(Author))]
    [TestFixture(typeof(Rental))]
    [TestFixture(typeof(Renter))]
    public class TestDocumentDbItemGetter<T> where T : Entity
    {
        private IDocumentDbItemGetter<T> documentDbItemGetter;
        private Mock<IDocumentClient> documentClientMock;
        private DocumentDbCredentials documentDbCredentials;

        private const string Guid1 = "7d89d8b6-302d-4f2d-83ee-2aa3c6e104f9";
        private const string Guid2 = "af90ca01-ccd5-4b2e-b7eb-ecc957d67f82";
        private const string Guid3 = "2d82f914-8d4a-4d3f-8ae0-2d4e9a462334";

        private readonly ResourceResponse<Document> resourceResponse1 = new ResourceResponse<Document>(new Document { Id = Guid1 });
        private readonly ResourceResponse<Document> resourceResponse2 = new ResourceResponse<Document>(new Document { Id = Guid2 });
        private readonly ResourceResponse<Document> resourceResponse3 = new ResourceResponse<Document>(new Document { Id = Guid3 });

        [SetUp]
        public void Setup()
        {
            documentClientMock = new Mock<IDocumentClient>();
            documentDbCredentials = new DocumentDbCredentials
            {
                AuthKey = string.Empty,
                CollectionId = string.Empty,
                DatabaseId = string.Empty,
                Endpoint = string.Empty
            };

            SetupDocumentClientMock(documentClientMock);

            documentDbItemGetter = new DocumentDbItemGetter<T>(documentDbCredentials, documentClientMock.Object);
        }

        private void SetupDocumentClientMock(Mock<IDocumentClient> mock)
        {
            if (mock == null) return;

            mock.Setup(t => t.ReadDocumentAsync(UriFactory.CreateDocumentUri(documentDbCredentials.DatabaseId,
                    documentDbCredentials.CollectionId, Guid1), null))
                .ReturnsAsync(resourceResponse1);

            mock.Setup(t => t.ReadDocumentAsync(UriFactory.CreateDocumentUri(documentDbCredentials.DatabaseId,
                            documentDbCredentials.CollectionId, Guid2), null))
                .Returns(Task.FromResult(resourceResponse2));


            DocumentClientException exception = CreateDocumentClientExceptionForTesting(new Error(),
                HttpStatusCode.NotFound);
            mock.Setup(t => t.ReadDocumentAsync(UriFactory.CreateDocumentUri(documentDbCredentials.DatabaseId,
                    documentDbCredentials.CollectionId, Guid3), null))
                .Throws(exception);
        }

        [TestCase(Guid1, ExpectedResult = Guid1)]
        [TestCase(Guid2, ExpectedResult = Guid2)]
        public string TestGetItemAsync(string id)
        {
            T item = documentDbItemGetter.GetItem(Guid.Parse(id));

            return item.Id.ToString();
        }

        [TestCase(Guid3, ExpectedResult = null)]
        public T TestGetItemAsyncNotFound(string id)
        {
            T item = documentDbItemGetter.GetItem(Guid.Parse(id));

            return item;
        }


        private static DocumentClientException CreateDocumentClientExceptionForTesting(
                                           Error error, HttpStatusCode httpStatusCode)
        {
            Type type = typeof(DocumentClientException);

            object documentClientExceptionInstance = type.Assembly.CreateInstance(type.FullName,
                false, BindingFlags.Instance | BindingFlags.NonPublic, null,
                new object[] { error, null, httpStatusCode }, null, null);

            return (DocumentClientException)documentClientExceptionInstance;
        }
    }
}
