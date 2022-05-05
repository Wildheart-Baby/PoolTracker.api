using PoolTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Players
{
    public interface IPlayersRepository
    {
        public Task<IEnumerable<Player>> GetPlayers();
        public Task<Player> CreatePlayer(Player player);
    }
}
