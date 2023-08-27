using UnityEngine;

namespace CeltaGames
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] Transform _player;

        void FixedUpdate()
        {
            var pos = transform.position;
            transform.position = new Vector3(pos.x, _player.position.y, pos.z)  ;
        }
    }
}
