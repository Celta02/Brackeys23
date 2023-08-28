using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CeltaGames
{
    [RequireComponent(typeof(BoxCollider))]
    public class VictoryCondition : MonoBehaviour
    {
        [SerializeField] DiveMeter _diveMeter;
        [SerializeField] RivalDiveMeter _rivalDiveMeter;
        [SerializeField] PlayerBehaviourRecord _record;

        SaveManager _saveManager;
        Collider _collider;
        float _bestRecord;
        bool _playerHasReached;
        bool _rivalHasReached;

        void Awake() => _collider = GetComponent<Collider>();
        void Start()
        {
            _saveManager = SaveManager.Instance;
            _collider.OnTriggerEnterAsObservable().Subscribe(OnReachingSurface).AddTo(this);
        }

        void OnReachingSurface(Collider col)
        {
            if (col.gameObject.TryGetComponent(out PlayersHead p))
            {
                _playerHasReached = true;
                p.StopSwimming();
            }

            if (col.gameObject.TryGetComponent(out RivalsHead r))
            {
                _rivalHasReached = true;
                r.StopSwimming();
            }

            if (_playerHasReached && _rivalHasReached)
                CheckResults();
        }

        async void CheckResults()
        {
            _rivalDiveMeter.RegisterRivalsMaxDepth();
            _saveManager.MaxDepth = _diveMeter.MaxDepth.Value;
            
            if (_diveMeter.MaxDepth.Value > _saveManager.GetBestScore())
            {
                SaveManager.Instance.BestScore = _diveMeter.MaxDepth.Value;
                _record.RegisterBehaviour();
            }
            await SaveManager.Instance.Save();

            if (_saveManager.GetMaxDepth() > _saveManager.GetRivalMaxDepth())
                GamePlayManager.Instance.Win(_saveManager.GetBestScore());
            else
                GamePlayManager.Instance.Defeat();
        }
    }
}