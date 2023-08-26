using DG.Tweening;
using UnityEngine;

namespace CeltaGames
{
    public class Diving : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] AnimationEvents _animationEvents;
        [SerializeField] Transform _model;
        [SerializeField] float _basalSpeed = 1.5f;
        
        [Header("Initial Speed")]
        [SerializeField] float _initialSpeed = 5f;
        [SerializeField] float _initialSpeedDuration = 2f;
        [SerializeField] Ease _initialSpeedEase = Ease.OutCubic;

        [Header("Breaststroke")]
        [SerializeField] float _strokeSpeed = 8f;
        [SerializeField] float _strokeFirstDuration = 1f;
        [SerializeField] float _strokeSecondDuration = 4f;
        [SerializeField] Ease _strokeFirstSpeedEase = Ease.InExpo;
        [SerializeField] Ease _strokeSecondSpeedEase = Ease.OutExpo;
        
        float _currentSpeed;
        bool _hasStopped;
        Tween _initialImpulseTween;
        Tween _strokeFirstTween;
        Tween _strokeSecondTween;
        
        void Start()
        {
            InitialImpulse();
            _animationEvents.BreaststrokeImpulse += BreaststrokeImpulse;
            _animationEvents.StartTurningAround += TurnAroundDeceleration;
            _animationEvents.StopTurningAround += TurnAroundImpulse;
            _animationEvents.StopSwimming += StopMovement;
        }
        
        void InitialImpulse()
        {
            _currentSpeed = _initialSpeed;
            _initialImpulseTween?.Kill();
            _initialImpulseTween = DOTween.To(() => _currentSpeed,
                    x => _currentSpeed = x,
                    _basalSpeed,
                    _initialSpeedDuration)
                .SetEase(_initialSpeedEase)
                .OnUpdate(() => _rigidbody.velocity = _currentSpeed * _model.up);
        }

        void BreaststrokeImpulse()
        {
            _initialImpulseTween?.Kill();
            BreaststrokeFirst();
        }
        void BreaststrokeFirst()
        {
            _strokeFirstTween?.Kill();
            _strokeFirstTween = DOTween.To(() => _currentSpeed,
                    x => _currentSpeed = x,
                    _strokeSpeed,
                    _strokeFirstDuration)
                .SetEase(_strokeFirstSpeedEase)
                .OnUpdate(() => _rigidbody.velocity = _currentSpeed * _model.up)
                .OnComplete(BreastStrokeSecond);
        }
        void BreastStrokeSecond()
        {
            _strokeSecondTween?.Kill();
            _strokeSecondTween = DOTween.To(() => _currentSpeed,
                    x => _currentSpeed = x,
                    _basalSpeed,
                    _strokeSecondDuration)
                .SetEase(_strokeSecondSpeedEase)
                .OnUpdate(() => _rigidbody.velocity = _currentSpeed * _model.up);
        }

        void TurnAroundDeceleration() => _rigidbody.velocity = Vector3.zero;

        void TurnAroundImpulse()
        {            
            BreaststrokeImpulse();
        }
        void StopMovement()
        {
            if (_hasStopped) return;
            _hasStopped = true;
            _initialImpulseTween.Kill();
            _strokeFirstTween.Kill();
            _strokeSecondTween.Kill();
            _rigidbody.velocity = Vector3.zero;
            transform.DOMoveY(0f, 0.5f);
            Debug.Log($"Stop");
        }

        void OnDestroy()
        {
            _initialImpulseTween?.Kill();
            _strokeFirstTween?.Kill();
            _strokeSecondTween?.Kill();
        }
    }
}
