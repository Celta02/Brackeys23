using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class BestScoreUI : MonoBehaviour
    {
        [SerializeField] SaveManager _saveManager;
        [SerializeField] TMP_Text _scoreText;

        void Start()
        {
            _scoreText.text =  $"{_saveManager.Load().BestScore:F2} m";
        }
    }
}