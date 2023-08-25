using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using static CeltaGames.WebUrlConstants;

namespace CeltaGames
{
    public class SaveLoad
    {
        readonly string _path;

        public SaveLoad(string path, MonoBehaviour handler) => _path = path;

        public void Save(SaveData data) => File.WriteAllText(_path,JsonUtility.ToJson(data));

        public async Task<SaveData> Load()
        {
            Debug.Log($"loading...");
            if (File.Exists(_path)) 
                return JsonUtility.FromJson<SaveData>(await File.ReadAllTextAsync(_path));
            
            var result = await WebUtils.Get(LoadDataUrl);
            return JsonUtility.FromJson<SaveData>(result);

        }
    }
}