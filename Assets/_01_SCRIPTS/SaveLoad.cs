using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.CloudSave;
using UnityEngine;


namespace CeltaGames
{
    public class SaveLoad
    {
        public async Task Save(SaveData data)
        {
            var paramData = new Dictionary<string, object> { { "SaveData", data } };
            await CloudSaveService.Instance.Data.ForceSaveAsync(paramData);
        }

        public async Task<SaveData> Load()
        {
            Debug.Log($"loading...");
            var dataDict = await CloudSaveService.Instance.Data.LoadAsync();

            var data = dataDict.ContainsKey("SaveData") 
                ? JsonConvert.DeserializeObject<SaveData>(dataDict["SaveData"]) 
                : new SaveData().DefaultData();
            
            Debug.Log($"Data Loaded");
            // File.WriteAllText(Application.persistentDataPath + "/LoadedDataTest.save", test);
            return data;
        }
    }
}