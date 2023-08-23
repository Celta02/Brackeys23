using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CeltaGames
{
    public class AIAnimationController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] GameTimer _timer;

        readonly SaveData _aiData = new(true);
        Queue<long> _strokesQueue;
        long _nextStroke;
        bool _isSwimmingBack;
        
        static readonly int Breaststroking = Animator.StringToHash("Breaststroking");
        static readonly int TurningAround = Animator.StringToHash("TurningAround");
        static readonly int IsSwimmingBack = Animator.StringToHash("IsSwimmingBack");

        void Start()
        {
            _strokesQueue = _aiData.StrokeTimes;
            _nextStroke = _strokesQueue.Dequeue();
            var timer = _timer.TimerObservable;
            timer.Where(x => x == _aiData.TurnAroundTime).Subscribe(TurnAround).AddTo(this);
            timer.Where(x => x == _nextStroke).Subscribe(Breaststroke).AddTo(this);
        }

        void Breaststroke(long time)
        {
            _nextStroke = _strokesQueue.Count >0? _strokesQueue.Dequeue():0;
            _animator.SetTrigger(Breaststroking);
        }

        void TurnAround(long time)
        {
            _animator.SetTrigger(TurningAround);
            _isSwimmingBack = !_isSwimmingBack;
            _animator.SetBool(IsSwimmingBack, _isSwimmingBack);
        }
    }
}