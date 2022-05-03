using Microsoft.AspNetCore.Mvc;
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
    }
}
