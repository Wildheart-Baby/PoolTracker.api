﻿using Dapper;
using PoolTracker.api.Context;
using PoolTracker.Repository.Entities;
using PoolTracker.Repository.Matches;
using System;
using System.Collections.Generic;
using System.Data;
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
                var matches = await connection.QueryAsync<PlayedMatch>(query);
                return matches.ToList();
            }
        }

        public async Task CreateMatch(Match match)
        {
            var query = "Insert into matches (player1_id, player2_id, winner_id, balls_left, matchEnding) Values (@player1_id, @player2_id, @winner_id, @balls_left, @matchEnding)";
            var parameters = new DynamicParameters();
            parameters.Add("player1_id", match.player1_id, DbType.Int32);
            parameters.Add("player2_id", match.player2_id, DbType.Int32);
            parameters.Add("winner_id", match.winner_id, DbType.Int32);
            parameters.Add("balls_left", match.balls_left, DbType.Int32);
            parameters.Add("matchEnding", match.matchEnding, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
