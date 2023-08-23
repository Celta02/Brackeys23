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
        [SerializeField] RivalDiveMeter _rivalDiveMeter;
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
            _rivalDiveMeter.SaveRivalsMaxDepth();
            
            var data = _saveManager.Load();
            _saveManager.MaxDepth = _diveMeter.MaxDepth.Value;
            _saveManager.BestScore = Mathf.Max(data.BestScore, _diveMeter.MaxDepth.Value);
            _saveManager.Save();
            data = _saveManager.Load();
            SceneManager.LoadScene(data.MaxDepth > data.RivalMaxDepth ? 2 : 4);
        }
    }
}