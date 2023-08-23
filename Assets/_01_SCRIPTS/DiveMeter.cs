using UniRx;
using UnityEngine;

namespace CeltaGames
{
    public class DiveMeter : MonoBehaviour
    {
        [SerializeField] Transform _player;
        [SerializeField] float _metersPerPixel = 0.25f;

        readonly FloatReactiveProperty _currentDepth = new();
        readonly FloatReactiveProperty _maxDepth = new();
        
        public FloatReactiveProperty CurrentDepth => _currentDepth;
        public FloatReactiveProperty MaxDepth => _maxDepth;

        void Update()
        {
            _currentDepth.Value = transform.position.y * -_metersPerPixel;
            _maxDepth.Value = Mathf.Max(_currentDepth.Value,_maxDepth.Value);
        }
    }
}