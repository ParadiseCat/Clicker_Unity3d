using System.Collections.Generic;

namespace Patterns
{
    /// <summary>
    /// Реализация шаблона объекта моделей
    /// </summary>
    public class AnimatedModel : IAnimatedModel<AnimatedModel.SkinMaterials, AnimatedModel.Animations>
    {
        private const string NAME = "Name";
        private const string RES_PATH = "Path";

        private Dictionary<SkinMaterials, string> dictSkinMaterials = new Dictionary<SkinMaterials, string>()
        {
            {SkinMaterials.Skin1, "skin1" },
            {SkinMaterials.Skin2, "skin2" },
            {SkinMaterials.Skin3, "skin3" }
        };

        private Dictionary<Animations, string> dictAnimations = new Dictionary<Animations, string>()
        {
            {Animations.Ani1, "ani1" },
            {Animations.Ani2, "ani2" },
            {Animations.Ani3, "ani3" }
        };

        public string Name
        {
            get
            {
                return NAME;
            }
        }

        public string Res_Path
        {
            get
            {
                return RES_PATH;
            }
        }

        public enum SkinMaterials
        {
            Skin1,
            Skin2,
            Skin3
        }

        public enum Animations
        {
            Ani1,
            Ani2,
            Ani3
        }

        public string SkinMaterial(SkinMaterials value)
        {
            return dictSkinMaterials[value];
        }

        public string Animation(Animations animation)
        {
            return dictAnimations[animation];
        }
    }
}
