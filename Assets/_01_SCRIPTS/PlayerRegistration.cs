using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class PlayerRegistration : MonoBehaviour
    {
        [SerializeField] GameObject _playerRegistration;
        [SerializeField] TMP_InputField _input;

        void Awake() => HideRegistration();
        void Start()
        {
            GamePlayManager.Instance.OpenRegisterNameEvent += ShowRegistration;
            GamePlayManager.Instance.CloseRegisterNameEvent += HideRegistration;
        }
        void ShowRegistration() => _playerRegistration.SetActive(true);
        void HideRegistration() => _playerRegistration.SetActive(false);
        public async void OnOk() => await GamePlayManager.Instance.RegisterName(_input.text);
    }
}