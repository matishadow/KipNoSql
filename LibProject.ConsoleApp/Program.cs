using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using LibProject.Core;
using LibProject.Interfaces;
using LibProject.Models;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace LibProject.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentDbCredentials documentDbCredentials = CreateDocumentDbCredentials();
            var documentClient = new DocumentClient(new Uri(documentDbCredentials.Endpoint), documentDbCredentials.AuthKey);

            var authorCreator = new DocumentDbItemCreator<Author>(documentDbCredentials, documentClient);
            var bookCreator = new DocumentDbItemCreator<Book>(documentDbCredentials, documentClient);
            var authorGetter = new DocumentDbItemGetter<Author>(documentDbCredentials, documentClient);
            var bookGetter = new DocumentDbItemGetter<Book>(documentDbCredentials, documentClient);

            InsertItems(authorCreator, bookCreator);

            Author patrickRothfuss = authorGetter.GetItems(author => author.Surname == "Rothfuss").Single();
            List<Book> patrickRothussBooks =
                bookGetter.GetItems(book => book.AuthorId.ToString() == patrickRothfuss.id.ToString()).ToList();

            Console.WriteLine(patrickRothfuss);
            patrickRothussBooks.ForEach(Console.WriteLine);

            Console.Read();
        }

        private static void InsertItems(IDocumentDbItemCreator<Author> authorCreator, IDocumentDbItemCreator<Book> bookCreator)
        {
            IEnumerable<Author> authors = GetAuthors();
            IEnumerable<Book> books = GetBooks();

            foreach (Author author in authors)
                authorCreator.CreateItem(author);
            foreach (Book book in books)
                 bookCreator.CreateItem(book);
        }

        private static IEnumerable<Author> GetAuthors()
        {
            string authorsJson = Properties.Resources.Authors;
            return JsonConvert.DeserializeObject<Author[]>(authorsJson);
        }

        private static IEnumerable<Book> GetBooks()
        {
            string booksJson = Properties.Resources.Books;
            return JsonConvert.DeserializeObject<Book[]>(booksJson);
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
