namespace Kip.Models.Base
{
    public class MovieDvd : Entity
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public int DurationTimeInMinutes { get; set; }
    }
}