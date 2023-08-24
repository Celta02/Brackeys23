using UnityEngine;

namespace CeltaGames
{
    public class RivalDiveMeter : MonoBehaviour
    {
        [SerializeField] AnimationEvents _animationEvents;
        [SerializeField] Transform _rival;

        float _maxDepth;
        float _metersPerPixel = 0.25f;

        void Start()
        {
            _animationEvents.StartTurningAround += RegisterRivalsMaxDepth;
        }

        public void RegisterRivalsMaxDepth()
        {
            _maxDepth = Mathf.Max(_maxDepth, _rival.position.y * -_metersPerPixel);
            SaveManager.Instance.RivalMaxDepth = _maxDepth;
        }
    }
}