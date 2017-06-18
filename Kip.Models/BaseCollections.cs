using System.Collections.Generic;
using Kip.Models.Base;

namespace Kip.Models
{
    public class BaseCollections
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<EBook> EBooks { get; set; }
        public IEnumerable<GameDisc> GameDiscs { get; set; }
        public IEnumerable<Magazine> Magazines { get; set; }
        public IEnumerable<MovieDvd> MovieDvds { get; set; }
        public IEnumerable<MusicCd> MusicCds { get; set; }
    }
}