﻿using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class BestScoreUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreText;

        void Start()
        {
            _scoreText.text =  $"{SaveManager.Instance.CurrentData.BestScore:F2} m";
        }
    }
}