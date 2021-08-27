using Clicker;
using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// Реализации обновляемого объекта для простого Monobehaviour
    /// </summary>
    public abstract class UpdateBehaviour : MonoBehaviour, IUpdate
    {
        /// <summary>
        /// инициализация обновляемого объекта
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetUpdateBehaviour(this);
            OnStart();
        }

        /// <summary>
        /// удаление обновляемого объекта
        /// </summary>
        private void OnDestroy()
        {
            GameManager.Instance.ResetUpdateBehaviour(this);
            OnDestroyEvent();
        }

        /// <summary>
        /// определение функционала при обновлении наследника
        /// </summary>
        public virtual void OnUpdate(float deltaTime) { }

        /// <summary>
        /// определение функционала при инициализации наследника
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// определение функционала при удалении наследника
        /// </summary>
        protected virtual void OnDestroyEvent() { }
    }
}