﻿using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class BestScoreUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreText;

        void Start() => GamePlayManager.Instance.ShowBestScoreEvent += UpdateScore;

        void UpdateScore() => _scoreText.text =  $"{SaveManager.Instance.GetBestScore():F2} m";
    }
}