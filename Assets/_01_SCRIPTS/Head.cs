using UnityEngine;

namespace CeltaGames
{
    public abstract class Head : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        static readonly int Swimming = Animator.StringToHash("StopSwimming");

        public void StopSwimming() => _animator.SetTrigger(Swimming);
    }
}