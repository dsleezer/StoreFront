using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        public int GenreId { get; set; }
        public string Genre1 { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
    }
}
