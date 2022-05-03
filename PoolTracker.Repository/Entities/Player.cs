using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Entities
{
    public class Player
    {
        public int id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public Boolean archived { get; set; }
        public int position { get; set; }
    }
}
