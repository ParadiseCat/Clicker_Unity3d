using Patterns;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker
{
    /// <summary>
    /// Контроллер сцены меню
    /// </summary>
    internal class MenuController : Controller<MenuController>
    {
        const int BUTTON_FONT_SIZE = 40;

        Dictionary<ButtonMenu, Rect> buttonRects;

        Color buttonColor;

        /// <summary>
        /// перечисление кнопок меню
        /// </summary>
        private enum ButtonMenu
        {
            NewGame,
            BestScores,
            Credits,
            Quit
        }

        /// <summary>
        /// инициализация контроллера
        /// </summary>
        public override void OnAwake()
        {
            buttonRects = new Dictionary<ButtonMenu, Rect>();
            buttonColor = new Color(1f, 1f, 0f);
            ButtonCreate();
        }

        /// <summary>
        /// вывод кнопок графического интерфейса
        /// </summary>
        private void OnGUI()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = BUTTON_FONT_SIZE;
            buttonStyle.padding = new RectOffset(0, 0, 0, 0);

            GUI.backgroundColor = buttonColor;

            GUIButtonDraw(buttonRects[ButtonMenu.NewGame], "New Game", buttonStyle, ButtonMenu.NewGame);
            GUIButtonDraw(buttonRects[ButtonMenu.BestScores], "Best scores", buttonStyle, ButtonMenu.BestScores);
            GUIButtonDraw(buttonRects[ButtonMenu.Credits], "Credits", buttonStyle, ButtonMenu.Credits);
            GUIButtonDraw(buttonRects[ButtonMenu.Quit], "Quit Game", buttonStyle, ButtonMenu.Quit);
        }

        /// <summary>
        /// регистрация события нажатия на кнопку
        /// </summary>
        private void GUIButtonDraw(Rect position, string text, GUIStyle style, ButtonMenu action)
        {
            if (GUI.Button(position, text, style))
            {
                ButtonAction(action);
            }
        }

        /// <summary>
        /// выполнение действия кнопки
        /// </summary>
        private void ButtonAction(ButtonMenu action)
        {
            switch (action)
            {
                case ButtonMenu.NewGame: GameManager.GoToScene(GameData.Scene.PlayScene); break;
                case ButtonMenu.BestScores: break;
                case ButtonMenu.Credits: break;
                case ButtonMenu.Quit: Application.Quit(); break;
            }
        }

        /// <summary>
        /// создание кнопок для GUI
        /// </summary>
        private void ButtonCreate()
        {
            List<ButtonMenu> listButtons = Enum.GetValues(typeof(ButtonMenu)).Cast<ButtonMenu>().ToList();

            Vector2 start = new Vector2(GameData.cameraWidthHalf - 200f, 200f);
            Vector2 size = new Vector2(400f, 150f);
            float dist = 30f;

            int count = 0;

            foreach (ButtonMenu button in listButtons)
            {
                buttonRects.Add(button, GetRect(start, size, dist, 0, count));
                count++;
            }

            Rect GetRect(Vector2 startPoint, Vector2 sizeButton, float distance, int posX, int posY)
            {
                return new Rect(
                    startPoint.x + posX * (sizeButton.x + distance),
                    startPoint.y + posY * (sizeButton.y + distance),
                    sizeButton.x,
                    sizeButton.y);
            }
        }
    }
}
