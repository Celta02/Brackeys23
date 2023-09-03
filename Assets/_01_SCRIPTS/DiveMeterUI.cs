using TMPro;
using UniRx;
using UnityEngine;

namespace CeltaGames
{
    public class DiveMeterUI : MonoBehaviour
    {
        [SerializeField] DiveMeter _diveMeter;
        [SerializeField] TMP_Text _currentDepthText;
        [SerializeField] TMP_Text _maxDepthText;

        bool _isUnderwater;

        void OnEnable() => GamePlayManager.Instance.EnteredToTheWaterEvent += OnEnteringToTheWater;
        void OnDisable() => GamePlayManager.Instance.EnteredToTheWaterEvent -= OnEnteringToTheWater;

        void Start()
        {
            _diveMeter.CurrentDepth.Where(_=>_isUnderwater).Subscribe(UpdateCurrentDepthText).AddTo(this);
            _diveMeter.MaxDepth.Where(_=>_isUnderwater).Subscribe(UpdateMaxDepthText).AddTo(this);
        }
        void OnEnteringToTheWater() => _isUnderwater = true;
        void UpdateCurrentDepthText(float depth)
        {
            _currentDepthText.text = $"{depth:F2} m";
        }
        void UpdateMaxDepthText(float depth)
        {
            _maxDepthText.text = $"{depth:F2} m";
        }
    }
}