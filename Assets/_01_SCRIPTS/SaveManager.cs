using System;
using System.IO;
using UnityEngine;

namespace CeltaGames
{
    public class SaveManager : MonoBehaviour
    {
        float _maxDepth;

        public float MaxDepth { set => _maxDepth = value; }
        
        SaveData _data = new();
        string _path;

        void Awake()
        {
            _path = $"{Application.persistentDataPath}/Brackeys23.save";
        }

        public void Save()
        {
            _data.MaxDepth = _maxDepth;

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
            return _data;
        }
    }
}