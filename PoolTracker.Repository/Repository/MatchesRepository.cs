using Dapper;
using PoolTracker.api.Context;
using PoolTracker.Repository.Entities;
using PoolTracker.Repository.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Repository
{
    public class MatchesRepository : IMatchesRepository
    {
        private readonly DapperContext _context;

        public MatchesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlayedMatch>> GetMatches()
        {
            var query = "select p1.name as player1, p2.name as player2, w.name as winner, m.balls_left as balls_left, m.match_ending as matchEnding from Matches as m inner join players as p1 on p1.id = m.player1_id " +
                        " inner join players as p2 on p2.id = m.player2_id" +
                        " inner join players as w on w.id = m.winner_id";
            
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<PlayedMatch>(query);
                return companies.ToList();
            }
        }
    }
}
