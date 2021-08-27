using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// Синглтон игры - управления всей игрой
    /// </summary>
    public abstract class Manager<T> : Controller<T> where T : Component
    {
        public override void OnAwake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
