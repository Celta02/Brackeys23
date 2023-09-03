using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace CeltaGames
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] UnityEvent<GameTimer> _startTimer;
        
        IObservable<long> _timerObservable;
        public IObservable<long> TimerObservable => _timerObservable;

        void OnEnable() => GamePlayManager.Instance.EnteredToTheWaterEvent += StartTimer;
        void OnDisable() => GamePlayManager.Instance.EnteredToTheWaterEvent -= StartTimer;
        
        void StartTimer()
        {
            _timerObservable = Observable.Interval(TimeSpan.FromSeconds(0.1f));
            _startTimer?.Invoke(this);
        }

    }
}