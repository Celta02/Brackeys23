using System;
using System.IO;
using UnityEngine;

namespace CeltaGames
{
    public class SaveManager : MonoBehaviour
    {
        float _maxDepth;
        float _rivalMaxDepth;
        float _bestScore;

        public float MaxDepth { set => _maxDepth = value; }
        public float RivalMaxDepth { set => _rivalMaxDepth = value; }
        public float BestScore { set => _bestScore = value; }
        
        SaveData _data = new();
        string _path;

        void Awake()
        {
            _path = $"{Application.persistentDataPath}/Brackeys23.save";
        }

        public void Save()
        {
            _data.MaxDepth = _maxDepth;
            _data.RivalMaxDepth = _rivalMaxDepth;
            _data.BestScore = _bestScore;

            var saveJson = JsonUtility.ToJson(_data);
            Debug.Log($"{saveJson} is Saved!");
            File.WriteAllText(_path, saveJson);
        }

        public SaveData Load()
        {
            if (!File.Exists(_path))
                return new SaveData();
        
            var loadJson = File.ReadAllText(_path);
            _data = JsonUtility.FromJson<SaveData>(loadJson);

            _maxDepth = _data.MaxDepth;
            _rivalMaxDepth = _data.RivalMaxDepth;
            _bestScore = _data.BestScore;
            return _data;
        }
    }
}