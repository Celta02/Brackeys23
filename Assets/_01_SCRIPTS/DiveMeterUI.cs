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
        void Start()
        {
            _diveMeter.CurrentDepth.Subscribe(UpdateCurrentDepthText).AddTo(this);
            _diveMeter.MaxDepth.Subscribe(UpdateMaxDepthText).AddTo(this);
        }
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