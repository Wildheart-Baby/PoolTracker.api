using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Entities
{
    public class PlayedMatch
    {
        public int id { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public int balls_left { get; set; }
        public string winner { get; set; }
        public string matchEnding { get; set; }
}
}
