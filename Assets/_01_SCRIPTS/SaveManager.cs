using System.Collections.Generic;
using UnityEngine;

namespace CeltaGames
{
    public class SaveManager : SingletonPersistent<SaveManager>
    {
        static SaveData data = new();
        SaveLoad _saveLoad;

        public static SaveData CurrentData => data;
        public float MaxDepth { set => data.MaxDepth = value; }
        public float RivalMaxDepth { set => data.RivalMaxDepth = value; }
        public float BestScore { set => data.BestScore = value; }
        public void TurnTimes(Queue<long> turnarounds){data.TurnTimes = new List<long>(turnarounds);}
        public void StrokeTimes(Queue<long> strokes) { data.StrokeTimes = new List<long>(strokes);}

        public override void Awake()
        {
            base.Awake();
            _saveLoad = new SaveLoad($"{Application.persistentDataPath}/Brackeys23.save");
        }
        void Start() => data = Load();
        public void Save() => _saveLoad.Save(data);
        public SaveData Load() => data = _saveLoad.Load();
    }
}