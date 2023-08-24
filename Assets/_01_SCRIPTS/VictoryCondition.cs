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

        void Awake() => _collider = GetComponent<Collider>();
        void Start()
        {
            _data = SaveManager.CurrentData;
            _collider.OnTriggerEnterAsObservable().Subscribe(OnReachingSurface).AddTo(this);
        }

        void OnReachingSurface(Collider col)
        {
            _rivalDiveMeter.RegisterRivalsMaxDepth();
            _data.MaxDepth = _diveMeter.MaxDepth.Value;
            
            if (_data.MaxDepth > _data.RivalMaxDepth)
                WinCondition();
            else
                DefeatCondition();
        }

        void WinCondition()
        {
            if (_diveMeter.MaxDepth.Value > _data.BestScore)
            {
                SaveManager.Instance.BestScore = _diveMeter.MaxDepth.Value;
                _record.RegisterBehaviour();
            }
            SaveManager.Instance.Save();
            SceneManager.LoadSceneAsync(2);
        }

        void DefeatCondition()
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
}