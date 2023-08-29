using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static CeltaGames.WebUrlConstants;

namespace CeltaGames
{
    public class LeaderboardManager
    {
        const string _SCORES_API = "/scores/";
        const string _SCORES_PLAYERS_API = "/scores/players/";
        const string _RANGE_API = "/range";
        public async Task<LeaderboardSingle> SubmitPlayerScore(string playerId, float score, string accessToken)
        {
            Debug.Log($"Submitting Score...");
            var path = new StringBuilder();
            path.Append(LeaderboardAPI);
            path.Append(_SCORES_PLAYERS_API);
            path.Append(playerId);
            var result = await WebUtils.PostScore(path.ToString(), new LeaderboardScore(score), accessToken);
            Debug.Log($"Score Submitted");
            return result;
        }

        public async Task<LeaderboardGroup> GetScoresRange(string playerId, string accessToken)
        {
            Debug.Log($"Retrieving Scores...");
            var path = new StringBuilder();
            path.Append(LeaderboardAPI);
            path.Append(_SCORES_PLAYERS_API);
            path.Append(playerId);
            path.Append(_RANGE_API);
            
            var result = await WebUtils.GetScoresRange(path.ToString(), accessToken);
            Debug.Log($"Scores Retrieved");
            return result;
        }
    }
}