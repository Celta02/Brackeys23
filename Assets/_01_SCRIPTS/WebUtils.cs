using System.Threading.Tasks;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest.Result;

namespace CeltaGames
{
    public static class WebUtils
    {
        public static async Task<string> Get(string url)
        {
            using var request = UnityWebRequest.Get(url);
            request.SendWebRequest();
            while (!request.isDone) await Task.Yield();

            return request.result is ConnectionError or ProtocolError ? 
                request.error : request.downloadHandler.text;
        }
        
    }
}