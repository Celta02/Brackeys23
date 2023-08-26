using System;
using System.Threading.Tasks;
using UnityEngine;


namespace CeltaGames
{
    public class GamePlayManager : SingletonPersistent<GamePlayManager>
    {
        public event Func<Task> CheckRegistration;
        public event Action OpenRegisterNameEvent = delegate {};
        public event Action CloseRegisterNameEvent = delegate {};
        public event Action ShowBestScoreEvent = delegate {};
        
        SaveData _data;
        
        async void Start() => await PlayerRegistration();

        async Task PlayerRegistration()
        {
            await (CheckRegistration?.Invoke() ?? Task.CompletedTask);
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
        
        
    }
}