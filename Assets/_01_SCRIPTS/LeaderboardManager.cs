using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static CeltaGames.WebUrlConstants;

namespace CeltaGames
{
    public class LeaderboardManager
    {
        const string _ADD_SCORE_API = "/scores/players/";
        public async Task<LeaderboardSingle> SubmitPlayerScore(string playerId, float score, string accessToken)
        {
            Debug.Log($"Submitting Score...");
            var path = new StringBuilder();
            path.Append(LeaderboardAPI);
            path.Append(_ADD_SCORE_API);
            path.Append(playerId);
            var result = await WebUtils.PostScore(path.ToString(), new LeaderboardScore(score), accessToken);
            Debug.Log($"Score Submitted");
            return result;
        }
    }
}