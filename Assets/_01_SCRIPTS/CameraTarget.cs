using UnityEngine;

namespace CeltaGames
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] Transform _player;

        void FixedUpdate()
        {
            transform.position = _player.position;
        }
    }
}
