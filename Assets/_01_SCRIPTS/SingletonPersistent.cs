using UnityEngine;

namespace CeltaGames
{
    public class SingletonPersistent <T> : MonoBehaviour where T : Component
    {
        static T instance;

        public static T Instance 
        {
            get{
                if (instance != null) return instance;
                
                var go = new GameObject { name = typeof(T).Name };
                instance = go.AddComponent<T>();
                return instance;
            }
        }

        void OnDestroy() 
        {
            if (instance == this) {
                instance = null;
            }
        }

        public virtual void Awake() 
        {
            if (instance == null) 
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else 
            {
                if (instance != this) 
                    Destroy(gameObject);
            }
        }
    }
}