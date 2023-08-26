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

        SaveData _data;
        Collider _collider;
        float _bestRecord;
        bool _playerHasReached;
        bool _rivalHasReached;

        void Awake() => _collider = GetComponent<Collider>();
        void Start()
        {
            _data = SaveManager.Instance.CurrentData;
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
            _data.MaxDepth = _diveMeter.MaxDepth.Value;
            
            if (_diveMeter.MaxDepth.Value > _data.BestScore)
            {
                SaveManager.Instance.BestScore = _diveMeter.MaxDepth.Value;
                _record.RegisterBehaviour();
            }
            await SaveManager.Instance.Save();
            
            if (_data.MaxDepth > _data.RivalMaxDepth)
                WinCondition();
            else
                DefeatCondition();
        }

        void WinCondition() => SceneManager.LoadSceneAsync(2);
        void DefeatCondition() => SceneManager.LoadSceneAsync(4);
    }
}