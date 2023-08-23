using System;
using UniRx;
using UnityEngine;

namespace CeltaGames
{
    public class GameTimer : MonoBehaviour
    {
        IObservable<long> _timerObservable;
        public IObservable<long> TimerObservable => _timerObservable;

        public void StartTimer()
        {
            _timerObservable = Observable.Interval(TimeSpan.FromSeconds(0.1f));
        }

        void Awake()
        {
            StartTimer();
        }
    }
}