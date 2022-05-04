using Microsoft.AspNetCore.Mvc;
using PoolTracker.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoolTracker.Repository.Matches;
using PoolTracker.Repository.Entities;

namespace PoolTracker.api.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchesRepository _matchesRepo;
        public MatchesController(IMatchesRepository matchesRepo)
        {
            _matchesRepo = matchesRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            try
            {
                var matches = await _matchesRepo.GetMatches();
                return Ok(matches);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] Match match)
        {
            try
            {
                var createdMatch = await _matchesRepo.CreateMatch(match);
                return CreatedAtRoute("MatchById", new { id = createdMatch.id }, createdMatch);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
