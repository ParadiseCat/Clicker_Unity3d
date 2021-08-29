using UnityEngine;

namespace Clicker
{
    /// <summary>
    /// Постоянные игровые параметры
    /// </summary>
    internal static class GameData
    {
        public enum Scene
        {
            MenuScene,
            PlayScene,
            OverScene
        }

        public static float scaleCamera = Camera.main.pixelHeight / (Camera.main.orthographicSize * 2);
        public static float cameraWidthHalf = Camera.main.orthographicSize * Camera.main.aspect * scaleCamera;

        public static int screenWidth = Screen.width;
        public static int screenHeight = Screen.height;

        public static Vector3 WorldToScreen(Vector3 vecScreen)
        {

            return Camera.main.WorldToScreenPoint(vecScreen);
        }
    }
}
