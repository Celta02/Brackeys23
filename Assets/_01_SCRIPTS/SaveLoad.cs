using System.IO;
using UnityEngine;

namespace CeltaGames
{
    public class SaveLoad
    {
        readonly string _path;

        public SaveLoad(string path) => _path = path;

        public void Save(SaveData data) => File.WriteAllText(_path,JsonUtility.ToJson(data));

        public SaveData Load() => !File.Exists(_path) ? 
            new SaveData() : JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));
    }
}