using League.api.Models;

namespace League.api.Services
{
    public interface IRiotService
    {
        Task<Player?> GetPlayerAsync(string summonerName);
        Task<List<Match>> GetMatchesAsync(string puuid); 
    }
}

