using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Game
    {
        public int GameId { get; set; }
        public int ProductId { get; set; }
        public int GameTypeId { get; set; }
        public int GenreId { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public virtual GameType? GameType { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual Product? Product { get; set; }
    }
}
