using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class GameType
    {
        public GameType()
        {
            Games = new HashSet<Game>();
        }

        public int GameTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
    }
}
