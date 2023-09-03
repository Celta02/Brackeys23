using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CeltaGames
{
    [RequireComponent(typeof(BoxCollider))]
    public class SurfaceTrigger : MonoBehaviour
    {
        readonly Subject<Unit> _checkResults = new();
        public IObservable<Unit> CheckResults => _checkResults;

        Collider _collider;
        bool _playerIsUnderwater;
        bool _rivalIsUnderwater;
        bool _playerHasReached;
        bool _rivalHasReached;

        void Awake() => _collider = GetComponent<Collider>();
        void Start() => _collider.OnTriggerEnterAsObservable().Subscribe(OnReachingSurface).AddTo(this);

        void OnReachingSurface(Collider col)
        {
            if (col.gameObject.TryGetComponent(out PlayersHead p))
            {
                if (_playerIsUnderwater)
                {
                    _playerIsUnderwater = false;
                    p.StopSwimming();
                    GamePlayManager.Instance.ArrivedToSurface();
                    _playerHasReached = true;
                }
                else
                {
                    _playerIsUnderwater = true;
                    p.StartSwimming();
                    GamePlayManager.Instance.EnteredToTheWater();
                }
            }
            if (col.gameObject.TryGetComponent(out RivalsHead r))
            {
                if (_rivalIsUnderwater)
                {
                    _rivalIsUnderwater = false;
                    r.StopSwimming();
                    _rivalHasReached = true;
                }
                else
                {
                    _rivalIsUnderwater = true;
                    r.StartSwimming();
                }
            }

            if (_playerHasReached && _rivalHasReached) _checkResults.OnNext(Unit.Default);
        }
    }
}