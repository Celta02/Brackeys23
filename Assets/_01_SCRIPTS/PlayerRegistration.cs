using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class PlayerRegistration : MonoBehaviour
    {
        [SerializeField] TMP_InputField _input;

        public void OnOk()
        {
            SaveManager.Instance.PlayerName = _input.text;
            SaveManager.Instance.Save();
        }
    }
}