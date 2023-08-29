using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class LeaderboardDisplaySingleUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmpRank;
        [SerializeField] TMP_Text _tmpName;
        [SerializeField] TMP_Text _tmpScore;
        
        LeaderboardDisplaySingle _playerData;

        public void ShowPlayerInfo(LeaderboardDisplaySingle displaySingle)
        {
            _tmpRank.text = displaySingle.Rank;
            _tmpName.text = displaySingle.Name;
            _tmpScore.text = displaySingle.Score;
        }
    }
}