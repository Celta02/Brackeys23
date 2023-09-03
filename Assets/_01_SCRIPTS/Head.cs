using UnityEngine;

namespace CeltaGames
{
    public abstract class Head : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        static readonly int Swimming = Animator.StringToHash("StopSwimming");
        static readonly int DiveJumping = Animator.StringToHash("DiveJumping");

        public void StopSwimming() => _animator.SetTrigger(Swimming);
        public void StartSwimming() => _animator.SetTrigger(DiveJumping);
    }
}