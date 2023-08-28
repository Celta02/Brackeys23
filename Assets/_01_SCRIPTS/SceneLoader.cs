using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CeltaGames
{
    public class SceneLoader
    {

        public async Task LoadStartScene() => await LoadSceneAsync(0);
        public async Task LoadMainScene() => await LoadSceneAsync(1);
        public async Task LoadVictoryScene() => await LoadSceneAsync(2);
        public async Task LoadDrownScene() => await LoadSceneAsync(3);
        public async Task LoadDefeatScene() => await LoadSceneAsync(4);
        // public async Task LoadLeaderboardScene() => await LoadSceneAsync(1);
        
        async Task LoadSceneAsync(int sceneIndex)
        {
            var scene = SceneManager.LoadSceneAsync(sceneIndex);
            while (!scene.isDone)
            {
                //float progress = Mathf.Clamp01(scene.progress / 0.9f);
                //progress filling animation
                await Task.Yield();
            }
        }
        
    }
}