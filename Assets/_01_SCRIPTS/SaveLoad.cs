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
            var data = new SaveData();
            Debug.Log($"loading...");
            var dataDict = await CloudSaveService.Instance.Data.LoadAsync();

            if (dataDict.ContainsKey("SaveData"))
                data = JsonConvert.DeserializeObject<SaveData>(dataDict["SaveData"]);

            return data;
        }
    }
}