using Dapper;
using PoolTracker.api.Context;
using PoolTracker.Repository.Entities;
using PoolTracker.Repository.LeaderBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Repository
{
    public class LeaderBoardRepository : ILeaderBoardRepository
    {
        private readonly DapperContext _context;

        public LeaderBoardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetLeaderBoard()
        {
            var query = ";WITH cte_player_wins (player_id, wins) AS ("+
	        "SELECT p.id AS PlayerId, count(1) AS Wins "+
            "FROM Matches as m " +
            "INNER JOIN Players as p on p.id = winner_id " +
            "GROUP BY p.id " +
            "), " +
            "cte_player_losses(player_id, losses) AS(" +
            "SELECT p.id AS PlayerId, count(1) AS Losses " +
            "FROM Matches as m " +
            "INNER JOIN Players as p on((p.id = m.player1_id) OR(p.id = m.player2_id)) " +
            "where m.winner_id != p.id " +
            "GROUP BY p.id " +
            "), " +
            "cte_balls_left(player_id, balls) AS(" +
            "SELECT p.id AS PlayerId, SUM(m.balls_left) AS Balls " +
            "FROM Matches as m " +
            "INNER JOIN Players as p on((p.id = m.player1_id) OR(p.id = m.player2_id)) " +
            "where m.winner_id = p.id " +
            "GROUP BY p.id " +
            "),  " +
            "cte_matches_played(player_id, played) as ( " +
            "SELECT p.id AS PlayerId, count(1) AS Played " +
            "FROM Matches as m " +
            "INNER JOIN Players as p on((p.id = m.player1_id) OR(p.id = m.player2_id)) " +
            "GROUP BY p.id " +
            ") " +
            "SELECT p.*, isnull(w.wins, 0) as wins,  isnull(l.losses, 0) as losses, isnull(b.balls, 0) as balls, (coalesce(w.wins, 0) * 3 + coalesce(l.losses, 0) + coalesce(b.balls, 0)) as points, ROW_NUMBER() OVER(ORDER BY(coalesce(w.wins, 0) * 3 + coalesce(l.losses, 0) + coalesce(b.balls, 0)) desc, wins desc, mp.played desc) as position " +
            "FROM players as p " +
            "left JOIN cte_player_wins as w on w.player_id = p.id " +
            "left JOIN cte_player_losses as l on l.player_id = p.id " +
            "left Join cte_balls_left as b on b.player_id = p.id " +
            "left Join cte_matches_played as mp on mp.player_id = p.id " +
            "Order by position asc";

            using (var connection = _context.CreateConnection())
            {
                var players = await connection.QueryAsync<Player>(query);
                return players.ToList();
            }
        }
    }
}
