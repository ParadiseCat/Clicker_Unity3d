using Patterns;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Clicker
{
    /// <summary>
    /// Управление игрой
    /// </summary>
    internal class GameManager : Manager<GameManager>, IUpdateManager
    {
        /// <summary>
        /// Список для хранения всех обновляемых экземпляров
        /// </summary>
        private static List<IUpdate> updateableInstances;

        /// <summary>
        /// Текущая сцена
        /// </summary>
        private static GameData.Scene currentScene;

        /// <summary>
        /// Обновление - главных игровой цикл для всех экземпляров
        /// </summary>
        private void Update()
        {
            if (updateableInstances != null)
            {
                float deltaTime = Time.deltaTime;

                for (int i = updateableInstances.Count - 1; i >= 0; i--)
                {
                    IUpdate updateableObj = updateableInstances[i];

                    updateableObj.OnUpdate(deltaTime);
                }
            }
        }

        /// <summary>
        /// Добавление обновляемого объекта в список
        /// </summary>
        public void SetUpdateBehaviour(IUpdate obj)
        {
            if (!updateableInstances.Contains(obj))
            {
                updateableInstances.Add(obj);
            }
        }
        

        /// <summary>
        /// Удаление обновляемого объекта в списке
        /// </summary>
        public void ResetUpdateBehaviour(IUpdate obj)
        {
            if (updateableInstances.Contains(obj))
            {
                updateableInstances.Remove(obj);
            }
        }

        /// <summary>
        /// инициализация менеджера
        /// </summary>
        public override void OnAwake()
        {
            base.OnAwake();

            updateableInstances = new List<IUpdate>();

            currentScene = GameData.Scene.MenuScene;
            GoToScene(GameData.Scene.MenuScene);
        }

        

        /// <summary>
        /// выполнение кода перехода и инициализации сцены
        /// </summary>
        public static void GoToScene(GameData.Scene scene)
        {
            if (currentScene != scene)
            {
                SceneManager.LoadScene((int)scene);
                currentScene = scene;
            }
        }
    }
}
