using UnityEngine;

namespace CeltaGames
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] Transform _player;

        void LateUpdate()
        {
            transform.position = _player.position;
        }
    }
}
