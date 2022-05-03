using Microsoft.AspNetCore.Mvc;
using PoolTracker.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoolTracker.Repository.Matches;


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
    }
}
