using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CeltaGames
{
    [RequireComponent(typeof(BoxCollider))]
    public class VictoryCondition : MonoBehaviour
    {
        [SerializeField] SaveManager _saveManager;
        [SerializeField] DiveMeter _diveMeter;
        Collider _collider;
        float _bestRecord;

        void Awake() => _collider = GetComponent<Collider>();
        void Start()
        {
            _bestRecord = _saveManager.Load().MaxDepth;
            _collider.OnTriggerEnterAsObservable().Subscribe(OnReachingSurface).AddTo(this);
        }

        void OnReachingSurface(Collider col)
        {
            if (_diveMeter.MaxDepth.Value >= _bestRecord)
            {
                _saveManager.MaxDepth = _diveMeter.MaxDepth.Value;
                _saveManager.Save();
            }
            SceneManager.LoadScene(2);
        }
    }
}