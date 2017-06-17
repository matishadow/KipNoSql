namespace Kip.Models.Base
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string ISBN { get; set; }
    }
}