using Patterns;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Clicker
{
    /// <summary>
    /// ���������� �����
    /// </summary>
    internal class GameManager : Manager<GameManager>, IUpdateManager
    {
        /// <summary>
        /// ������ ��� �������� ���� ����������� �����������
        /// </summary>
        private static List<IUpdate> updateableInstances;

        /// <summary>
        /// ������� �����
        /// </summary>
        private static GameData.Scene currentScene;

        /// <summary>
        /// ���������� - ������� ������� ���� ��� ���� �����������
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
        /// ���������� ������������ ������� � ������
        /// </summary>
        public void SetUpdateBehaviour(IUpdate obj)
        {
            if (!updateableInstances.Contains(obj))
            {
                updateableInstances.Add(obj);
            }
        }
        

        /// <summary>
        /// �������� ������������ ������� � ������
        /// </summary>
        public void ResetUpdateBehaviour(IUpdate obj)
        {
            if (updateableInstances.Contains(obj))
            {
                updateableInstances.Remove(obj);
            }
        }

        /// <summary>
        /// ������������� ���������
        /// </summary>
        public override void OnAwake()
        {
            base.OnAwake();

            updateableInstances = new List<IUpdate>();

            currentScene = GameData.Scene.MenuScene;
            GoToScene(GameData.Scene.MenuScene);
        }

        

        /// <summary>
        /// ���������� ���� �������� � ������������� �����
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
