using PoolTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoolTracker.Repository.Matches
{
    public interface IMatchesRepository
    {
        public Task<IEnumerable<PlayedMatch>> GetMatches();
        public Task<Match> CreateMatch(Match match);
    }
}
