using Microsoft.AspNetCore.Mvc;
using PoolTracker.Repository.LeaderBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.api.Controllers
{
    [Route("api/leaderboard")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly ILeaderBoardRepository _leaderboardRepo;
        public LeaderBoardController(ILeaderBoardRepository leaderboardRepo)
        {
            _leaderboardRepo = leaderboardRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var leaderboard = await _leaderboardRepo.GetLeaderBoard();
                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
