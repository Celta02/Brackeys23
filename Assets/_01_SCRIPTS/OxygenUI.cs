using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CeltaGames
{
    public class OxygenUI : MonoBehaviour
    {
        [SerializeField] Oxygen _oxygen;
        [SerializeField] Image _oxygenBar;

        float _maxOxygen;
        
        void Start()
        {
            _maxOxygen = _oxygen.InitialOxygen;
            _oxygen.CurrentOxygenReactive.Subscribe(UpdateOxygen).AddTo(this);
        }

         void UpdateOxygen(float currentOxygen)
         {
             _oxygenBar.fillAmount = currentOxygen / _maxOxygen;
         }
    }
}