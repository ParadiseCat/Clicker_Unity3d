using Patterns;
using UnityEngine;

namespace Clicker
{
    public class GameState : Manager<GameState>
    {
        bool active;
        bool showInfo;

        Texture2D texTable;
        int texWidth;
        int texHeigth;

        public bool ShowInfo
        {
            get
            {
                return showInfo;
            }

            set
            {
                showInfo = value;
                SetTableInfo();
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }

        public void OnGUI()
        {
            if (active && showInfo)
            {
                GUI.DrawTextureWithTexCoords(new Rect(0f, 0f, texWidth, texHeigth), texTable, new Rect(0f, 0f, 1f, 1f));
            }
        }

        private void SetTableInfo()
        {
            if (!active)
            {
                texTable = TextureExtractFromSprite("Sprites/Counter");
                texWidth = texTable.width;
                texHeigth = texTable.height;

                active = true;
            }
        }

        private Texture2D TextureExtractFromSprite(string source)
        {
            int textureWidth;
            int textureHeight;

            Sprite sprite;
            Texture2D textureExtract;
            Texture2D textureSprite;
            RenderTexture renderExtract;
            RenderTexture renderSprite;

            sprite = Resources.Load<Sprite>(source);

            Debug.Log("SPRITE = " + sprite.ToString());

            if (sprite == null)
            {
                Application.Quit();
            }

            textureSprite = sprite.texture;
            textureWidth = textureSprite.width;
            textureHeight = textureSprite.height;

            renderSprite = RenderTexture.GetTemporary(textureWidth, textureHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
            Graphics.Blit(textureSprite, renderSprite);

            renderExtract = RenderTexture.active;
            RenderTexture.active = renderSprite;

            textureExtract = new Texture2D(textureWidth, textureHeight);
            textureExtract.filterMode = FilterMode.Point;
            textureExtract.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
            textureExtract.Apply();

            RenderTexture.active = renderExtract;
            RenderTexture.ReleaseTemporary(renderSprite);

            return textureExtract;
        }
    }
}