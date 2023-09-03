using UnityEngine;

namespace CeltaGames
{
    public class GravityHandler : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] float _initialSpeed = 10f;

        void Awake() => _rigidbody = GetComponent<Rigidbody>();

        void OnEnable() => GamePlayManager.Instance.EnteredToTheWaterEvent += StopGravity;
        void OnDisable() => GamePlayManager.Instance.EnteredToTheWaterEvent -= StopGravity;
        void Start()
        {
            _rigidbody.velocity =  -_initialSpeed * Vector3.up;
            _rigidbody.useGravity = true;
        }

        void StopGravity() => _rigidbody.useGravity = false;
    }
}