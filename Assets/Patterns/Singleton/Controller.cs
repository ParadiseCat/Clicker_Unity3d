using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// Сингтон контроллера (сцены) - управление в рамках сцены
    /// </summary>
    public abstract class Controller<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// переменная синглтона
        /// </summary>
        private static T instance;

        /// <summary>
        /// свойство получения синглтона объектами
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// инициализация синглтона
        /// </summary>
        public void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                OnAwake();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// определение функцианала инициализации для наследников
        /// </summary>
        public virtual void OnAwake() { }
    }
}
