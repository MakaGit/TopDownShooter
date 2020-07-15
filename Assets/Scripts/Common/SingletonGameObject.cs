using UnityEngine;

namespace TopDownShooter
{
    public class SingletonGameObject<T> : MonoBehaviour where T : SingletonGameObject<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    var holderObject = new GameObject($"Singleton_{typeof(T)}");
                    _instance = holderObject.AddComponent<T>();
                    //DontDestroyOnLoad(holderObject);
                }

                return _instance;
            }
        }

        public static T TryInstance { get { return _instance != null ? _instance : null; } }

        private static T _instance = null;

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
                Debug.LogError($"Singleton of type {typeof(T)} already exists in the scene");

            _instance = (T)this;
            //DontDestroyOnLoad(gameObject);
        }
    }
}
