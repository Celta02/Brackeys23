using System;
using System.Threading.Tasks;

namespace CeltaGames
{
    public class GamePlayManager : SingletonPersistent<GamePlayManager>
    {
        // public event Func<Task> CheckRegistration;
        public event Action OpenRegisterNameEvent = delegate {};
        public event Action CloseRegisterNameEvent = delegate {};
        public event Action ShowBestScoreEvent = delegate {};
        
        static string playerId;
        
        SaveData _data;
        readonly SceneLoader _sceneLoader = new SceneLoader();

        async void Start() => await PlayerRegistration();

        async Task PlayerRegistration()
        {
            // await (CheckRegistration.Invoke() ?? Task.CompletedTask);
            await AuthManager.RegisterAsync();
            _data = await SaveManager.Instance.Load();
            if (_data.PlayerName == "")
                OpenRegisterNameEvent?.Invoke();
            else
                ShowBestScoreEvent?.Invoke();
        }

        public async Task RegisterName(string playerName)
        {
            SaveManager.Instance.PlayerName = playerName;
            await SaveManager.Instance.Save();
            CloseRegisterNameEvent?.Invoke();
            ShowBestScoreEvent?.Invoke();
        }
        public static void SavePlayerId(string id) => playerId = id;
        
        public async void LoadStartScene() => await _sceneLoader.LoadStartScene();
        public async void LoadMainScene() => await _sceneLoader.LoadMainScene();
        public async void LoadVictoryScene() => await _sceneLoader.LoadVictoryScene();
        public async void LoadDrownScene() => await _sceneLoader.LoadDrownScene();
        public async void LoadDefeatScene() => await _sceneLoader.LoadDefeatScene();
    }
}