using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CeltaGames
{
    [RequireComponent(typeof(BoxCollider))]
    public class VictoryCondition : MonoBehaviour
    {
        Collider _collider;

        void Awake() => _collider = GetComponent<Collider>();
        void Start() => _collider.OnTriggerEnterAsObservable().Subscribe(OnReachingSurface).AddTo(this);
        void OnReachingSurface(Collider col) => SceneManager.LoadScene(2);
    }
}