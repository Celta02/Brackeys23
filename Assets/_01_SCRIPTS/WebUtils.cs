using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest.Result;

namespace CeltaGames
{
    public static class WebUtils
    {
        public static async Task<string> Get(string url)
        {
            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type","application/json");
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            return request.result is ConnectionError or ProtocolError ? 
                request.error : request.downloadHandler.text;
        }

        public static async Task<LeaderboardSingle> PostScore(string path, LeaderboardScore score, string accessToken)
        {
            try
            {
                var postData = JsonConvert.SerializeObject(score);
                using var request = UnityWebRequest.Post(path, postData, "number<double>");
                request.SetRequestHeader("Content-Type","application/json");
                request.SetRequestHeader("Authorization",$"Bearer {accessToken}");
                var operation = request.SendWebRequest();
                
                while (!operation.isDone) await Task.Yield();

                if (request.result is ConnectionError or ProtocolError)
                {
                    Debug.LogError($"error: {request.error}");
                    await File.WriteAllTextAsync(Application.persistentDataPath + "/LeaderboardConnectionError.save", request.downloadHandler.text);
                }
                
                var result = JsonConvert.DeserializeObject<LeaderboardSingle>(request.downloadHandler.text);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

    }
}