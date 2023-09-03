using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace CeltaGames
{
    public class Oxygen : MonoBehaviour
    {
        [SerializeField] float _initialOxygen = 10000f;
        [SerializeField] float _oxygenConsumption = 0.2f;
        [SerializeField] float _strokeOxygenConsumption = 5f;
        [SerializeField] AnimationEvents _animationEvents;
        
        PlayerControls _controls;
        bool _isInApnea = true;
        
        readonly FloatReactiveProperty _currentOxygen = new();
        public FloatReactiveProperty CurrentOxygenReactive => _currentOxygen;
        public float InitialOxygen => _initialOxygen;

        
        void OnEnable()
        {
            _controls.Enable();
            GamePlayManager.Instance.EnteredToTheWaterEvent += StartApnea;
        }

        void OnDisable()
        {
            _controls.Disable();
            GamePlayManager.Instance.EnteredToTheWaterEvent -= StartApnea;
        }

        void Awake() => _controls = new PlayerControls();

        void Start()
        {
            _currentOxygen.Value = _initialOxygen;
            _controls.Player.Breaststroke.started += StrokeOxygenConsumption;
            _animationEvents.StopSwimming += StopOxygenConsumption;
        }

        void StartApnea()
        {
            Observable.EveryUpdate().Where(x=>_isInApnea).Subscribe(x=>ReduceOxygen(_oxygenConsumption)).AddTo(this);
        }
        void StrokeOxygenConsumption(InputAction.CallbackContext obj)
        {
            ReduceOxygen(_strokeOxygenConsumption);
        }
        
        void ReduceOxygen(float consumption)
        {
            _currentOxygen.Value -= consumption;
            
            if (!(_currentOxygen.Value <= 0f)) return;
            GamePlayManager.Instance.LoadDrownScene();
            StopOxygenConsumption();
        }
        void StopOxygenConsumption() => _isInApnea = false;
    }
}
