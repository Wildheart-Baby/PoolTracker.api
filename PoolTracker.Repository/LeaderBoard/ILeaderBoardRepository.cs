using PoolTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.LeaderBoard
{
    public interface ILeaderBoardRepository
    {
        public Task<IEnumerable<Player>> GetLeaderBoard();
    }
}
