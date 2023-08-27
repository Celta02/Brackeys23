using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CeltaGames
{
    [RequireComponent(typeof(Animator), typeof(BoxCollider))]
    public class FishAnimationStart : MonoBehaviour
    {
        Animator _animator;
        Collider _collider;
        static readonly int TriggerAction = Animator.StringToHash("TriggerAction");

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
        }

        void Start() => _collider.OnTriggerEnterAsObservable().Subscribe(StartAnimation).AddTo(this);

        void StartAnimation(Collider col) => _animator.SetTrigger(TriggerAction);
    }
}