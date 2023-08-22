using UnityEngine;
using UnityEngine.InputSystem;

namespace CeltaGames
{
    public class SwimmingAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;

        PlayerControls _controls;
        bool _isSwimmingBack;
        
        static readonly int Breaststroking = Animator.StringToHash("Breaststroking");
        static readonly int TurningAround = Animator.StringToHash("TurningAround");
        static readonly int IsSwimmingBack = Animator.StringToHash("IsSwimmingBack");

        void OnEnable() => _controls.Enable();
        void OnDisable() => _controls.Disable();

        void Awake() => _controls = new PlayerControls();

        void Start()
        {
            _controls.Player.Breaststroke.started += TriggerBreaststrokeAnimation;
            _controls.Player.TurnBack.started += TriggerTurnBackAnimation;
        }
        void TriggerBreaststrokeAnimation(InputAction.CallbackContext obj) => _animator.SetTrigger(Breaststroking);
        void TriggerTurnBackAnimation(InputAction.CallbackContext obj)
        {
            Debug.Log($"Turning Back Triggered");
            _animator.SetTrigger(TurningAround);
            _isSwimmingBack = !_isSwimmingBack;
            _animator.SetBool(IsSwimmingBack, _isSwimmingBack);
        }
    }
}