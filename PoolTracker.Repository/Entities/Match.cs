using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Entities
{
    public class Match
    {
        public int id { get; set; }
        public int player1_id { get; set; }
        public int player2_id { get; set; }
        public int balls_left { get; set; }
        public int winner_id { get; set; }
        public string matchEnding { get; set; }
    }
}
