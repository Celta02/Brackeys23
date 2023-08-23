using UnityEngine;

namespace CeltaGames
{
    public class RivalDiveMeter : MonoBehaviour
    {
        [SerializeField] SaveManager _saveManager;
        [SerializeField] AnimationEvents _animationEvents;
        [SerializeField] Transform _rival;

        float _maxDepth;
        float _metersPerPixel = 0.25f;

        void Start()
        {
            _animationEvents.StartTurningAround += SaveRivalsMaxDepth;
        }

        public void SaveRivalsMaxDepth()
        {
            _maxDepth = Mathf.Max(_maxDepth, _rival.position.y * -_metersPerPixel);
            _saveManager.RivalMaxDepth = _maxDepth;
            _saveManager.Save();
        }
    }
}