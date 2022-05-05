using Dapper;
using PoolTracker.api.Context;
using PoolTracker.Repository.Entities;
using PoolTracker.Repository.Players;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Player> CreatePlayer(Player player)
        {
            var query = "Insert into players (name, photo) Values (@name, @photo);SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("name", player.name, DbType.String);
            parameters.Add("photo", player.photo, DbType.String);
            parameters.Add("archived", player.archived, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = 0;
                try
                {
                    id = await connection.QuerySingleAsync<int>(query, parameters);
                }
                catch (Exception ex) { var a = ex; }

                var createdPlayer = new Player
                {
                    id = id,
                    name = player.name,
                    photo = player.photo,
                    archived = player.archived
                };
                return createdPlayer;
            }
        }
                
    }
}
