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
            var query = "select * from Players where archived = 0";

            using (var connection = _context.CreateConnection())
            {
                var players = await connection.QueryAsync<Player>(query);
                return players.ToList();
            }
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            var query = "Insert into players (name, photo, archived) Values (@name, @photo, @archived);SELECT CAST(SCOPE_IDENTITY() as int)";
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

        public async Task ArchivePlayer(int player)
        {
            var query = "Update players Set archived = 1 where id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", player, DbType.Int32);            

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    Console.WriteLine(query);
                    await connection.ExecuteAsync(query, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }        
    }
}
