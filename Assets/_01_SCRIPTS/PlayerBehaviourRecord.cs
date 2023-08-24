using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CeltaGames
{
    public class PlayerBehaviourRecord : MonoBehaviour
    {
        PlayerControls _controls;
        long _currentInterval;
        
        readonly Queue<long> _strokeTimes = new();
        readonly Queue<long> _turnTimes = new();

        public Queue<long> StrokeTimes => _strokeTimes;
        public Queue<long> TurnTimes => _turnTimes;

        void OnEnable() => _controls.Enable();
        void OnDisable() => _controls.Disable();
        void Awake() => _controls = new PlayerControls();

        void Start()
        {
            _controls.Player.Breaststroke.started += RegisterBreaststroke;
            _controls.Player.TurnBack.started += RegisterTurnAround;
        }
        public void SubscribeToTimer(GameTimer timer)
        {
            timer.TimerObservable.Subscribe(UpdateCurrentInterval).AddTo(this);
        }

        void UpdateCurrentInterval(long currentInterval) => _currentInterval = currentInterval;
        void RegisterBreaststroke(InputAction.CallbackContext obj) => _strokeTimes.Enqueue(_currentInterval);
        void RegisterTurnAround(InputAction.CallbackContext obj) => _turnTimes.Enqueue(_currentInterval);

        public void RegisterBehaviour()
        {
            SaveManager.Instance.TurnTimes(_turnTimes);
            SaveManager.Instance.StrokeTimes(_strokeTimes);
        }
    }
}