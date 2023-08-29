using System;
using UnityEngine;

namespace CeltaGames
{
    public class LeaderboardUI : MonoBehaviour
    {
        [SerializeField] Transform _frame;
        [SerializeField] LeaderboardDisplaySingleUI _displaySingleUI;

        void Start() => GamePlayManager.Instance.ShowLeaderboardEvent += LoadLeaderboardData;

        void LoadLeaderboardData(LeaderboardGroup leaderGroup, string playerId)
        {
            foreach (var playerData in leaderGroup.results)
            {
                var playerName = playerData.playerName;
                var i =playerName.IndexOf("#", StringComparison.Ordinal);
                if (i > 0) playerName = playerName.Remove(i, playerName.Length - i);

                var displaySingle = new LeaderboardDisplaySingle(
                    playerData.rank.ToString()
                    ,playerName
                    ,$"{playerData.score+1:F2}");

                var displaySingleUI = Instantiate(_displaySingleUI, _frame);
                displaySingleUI.ShowPlayerInfo(displaySingle);
            }
            
        }
    }
}