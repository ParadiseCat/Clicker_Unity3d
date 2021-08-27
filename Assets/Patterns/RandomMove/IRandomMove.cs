using UnityEngine;

namespace Patterns
{
    public interface IRandomMove
    {
        // при Awake - вызов IRandomMoveble AddRandomMove
        // при Destroy - вызов IRandomMoveble RemoveRandomMove

        /// <summary>
        /// Запускает итерацию движение и проверяет отведённое время
        /// </summary>
        public bool MoveTo();

        /// <summary>
        /// Определяет направление и скорость движения и отведённое время
        /// </summary>
        public void SetDirection(Vector3 direction, int steps);

        public void InstanceDestroy();
    }
}
