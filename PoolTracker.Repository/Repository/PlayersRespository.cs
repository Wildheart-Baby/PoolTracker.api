using Dapper;
using PoolTracker.api.Context;
using PoolTracker.Repository.Entities;
using PoolTracker.Repository.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Repository
{
    public class PlayersRespository : IPlayersRepository
    {
        private readonly DapperContext _context;

        public PlayersRespository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var query = "select * from Players";

            using (var connection = _context.CreateConnection())
            {
                var players = await connection.QueryAsync<Player>(query);
                return players.ToList();
            }
        }
    }
}
