using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CeltaGames
{
    [RequireComponent(typeof(SurfaceTrigger))]
    public class VictoryCondition : MonoBehaviour
    {
        [SerializeField] DiveMeter _diveMeter;
        [SerializeField] RivalDiveMeter _rivalDiveMeter;
        [SerializeField] PlayerBehaviourRecord _record;

        SaveManager _saveManager;
        SurfaceTrigger _surfaceTrigger;
        float _bestRecord;

        void Awake() => _surfaceTrigger = GetComponent<SurfaceTrigger>();
        void Start()
        {
            _saveManager = SaveManager.Instance;
            _surfaceTrigger.CheckResults.Subscribe(_ => CheckResults()).AddTo(this);
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