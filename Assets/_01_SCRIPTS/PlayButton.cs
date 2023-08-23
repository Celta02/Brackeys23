using UnityEngine;
using UnityEngine.SceneManagement;

namespace CeltaGames
{
    public class PlayButton : MonoBehaviour
    {
        public void OnPlayButtonClicked() => SceneManager.LoadScene(1);
    }
}