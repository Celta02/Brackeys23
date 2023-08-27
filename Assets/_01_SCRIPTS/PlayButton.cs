using UnityEngine;

namespace CeltaGames
{
    public class PlayButton : MonoBehaviour
    {
        public void OnPlayButtonClicked() => GamePlayManager.Instance.LoadMainScene();
    }
}