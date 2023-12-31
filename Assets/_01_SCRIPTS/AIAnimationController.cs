﻿using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CeltaGames
{
    public class AIAnimationController : MonoBehaviour
    {
        [SerializeField] Animator _animator;

        SaveManager _saveManager;
        Queue<long> _strokesQueue;
        Queue<long> _turnQueue;
        long _nextStroke;
        long _nextTurn;
        bool _isSwimmingBack;
        
        static readonly int Breaststroking = Animator.StringToHash("Breaststroking");
        static readonly int TurningAround = Animator.StringToHash("TurningAround");
        static readonly int IsSwimmingBack = Animator.StringToHash("IsSwimmingBack");

        public void SubscribeToTimer(GameTimer timer)
        {
            Initialize();
            var timerObs = timer.TimerObservable;
            timerObs.Where(x => x == _nextTurn).Subscribe(TurnAround).AddTo(this);
            timerObs.Where(x => x == _nextStroke).Subscribe(Breaststroke).AddTo(this);
        }

        void Initialize()
        {
            _saveManager = SaveManager.Instance;
            _strokesQueue = new Queue<long>(_saveManager.GetStrokeTimes());
            _nextStroke = _strokesQueue.Count >0? _strokesQueue.Dequeue():0;   
            _turnQueue = new Queue<long>(_saveManager.GetTurnTimes());
            _nextTurn = _turnQueue.Dequeue();
        }

        void Breaststroke(long time)
        {
            _nextStroke = _strokesQueue.Count >0? _strokesQueue.Dequeue():0;
            _animator.SetTrigger(Breaststroking);
        }

        void TurnAround(long time)
        {
            _nextTurn = _turnQueue.Count >0? _turnQueue.Dequeue():0;
            _animator.SetTrigger(TurningAround);
            _isSwimmingBack = !_isSwimmingBack;
            _animator.SetBool(IsSwimmingBack, _isSwimmingBack);
        }
    }
}