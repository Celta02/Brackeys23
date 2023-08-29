using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CeltaGames
{
    public class GamePlayManager : SingletonPersistent<GamePlayManager>
    {
        // public event Func<Task> CheckRegistration;
        public event Action OpenRegisterNameEvent = delegate {};
        public event Action CloseRegisterNameEvent = delegate {};
        public event Action StartMainSceneEvent = delegate {};
        public event Action ArrivedToSurfaceEvent = delegate {};
        public event Action StartVictorySceneEvent = delegate {};
        public event Action StartDefeatSceneEvent = delegate {};
        public event Action StartDrownSceneEvent = delegate {};
        public event Action ShowBestScoreEvent = delegate {};
        public event Action<LeaderboardGroup,string> ShowLeaderboardEvent = delegate {};
        
        static string playerId;
        static string accessToken;
        
        SaveData _data;
        readonly SceneLoader _sceneLoader = new SceneLoader();
        readonly LeaderboardManager _leaderboard = new LeaderboardManager();

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
        public static void SaveAccessToken(string token) => accessToken = token;

        public async void LoadStartScene() => await _sceneLoader.LoadStartScene();
        public async void LoadMainScene()
        {
            await _sceneLoader.LoadMainScene();
            StartMainSceneEvent?.Invoke();
        }

        async Task LoadVictoryScene() => await _sceneLoader.LoadVictoryScene();
        public async void LoadDrownScene() => await _sceneLoader.LoadDrownScene();
        async void LoadDefeatScene() => await _sceneLoader.LoadDefeatScene();

        public void ArrivedToSurface() => ArrivedToSurfaceEvent?.Invoke();
        public async void Win(float bestScore)
        {
            var playerResults =await _leaderboard.SubmitPlayerScore(playerId, bestScore, accessToken);
            await LoadVictoryScene();
            var scoresRange = await _leaderboard.GetScoresRange(playerId, accessToken);
            ShowLeaderboardEvent?.Invoke(scoresRange, playerId);
        }

        public void Defeat() => LoadDefeatScene();
    }
}