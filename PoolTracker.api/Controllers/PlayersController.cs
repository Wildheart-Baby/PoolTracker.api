using Microsoft.AspNetCore.Mvc;
using PoolTracker.Repository.Entities;
using PoolTracker.Repository.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.api.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepo;
        public PlayersController(IPlayersRepository playersRepo)
        {
            _playersRepo = playersRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var players = await _playersRepo.GetPlayers();
                return Ok(players);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] Player player)
        {
            try
            {
                var createdPlayer = await _playersRepo.CreatePlayer(player);
                return CreatedAtRoute("PlayerById", new { id = createdPlayer.id }, createdPlayer);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPut]
        [Route("archive")]
        public async Task<IActionResult> ArchivePlayer([FromBody] int id)
        {
            try
            {
                await _playersRepo.ArchivePlayer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
