using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CeltaGames
{
    public class SaveManager : SingletonPersistent<SaveManager>
    {
        SaveData _data = new SaveData();
        SaveLoad _saveLoad;

        public SaveData CurrentData => _data;
        public string PlayerId { set => _data.PlayerID = value; }
        public string PlayerName { set => _data.PlayerName = value; }
        public float MaxDepth { set => _data.MaxDepth = value; }
        public float RivalMaxDepth { set => _data.RivalMaxDepth = value; }
        public float BestScore { set => _data.BestScore = value; }
        public void TurnTimes(Queue<long> turnarounds){_data.TurnTimes = new List<long>(turnarounds);}
        public void StrokeTimes(Queue<long> strokes) { _data.StrokeTimes = new List<long>(strokes);}

        public override void Awake()
        {
            base.Awake();
            _saveLoad = new SaveLoad($"{Application.persistentDataPath}/Brackeys23.save",this);
        }
        async void Start()
        {
            _data = await Load();
            Debug.Log($"Best Saved Score: {_data.BestScore}");
        }

        public void Save() => _saveLoad.Save(_data);
        public async Task<SaveData> Load() => _data = await _saveLoad.Load();
    }
}