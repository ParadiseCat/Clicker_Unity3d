using Patterns;
using System.Collections.Generic;

namespace Clicker.ResourcesData
{
    public class Mushroom : IAnimatedModel<Mushroom.SkinMaterials, Mushroom.Animations>
    {
        private const string RES_PATH = "Animation/Mushroom/";
        private const string NAME = "Mushroom";

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
            Blue,
            Red,
            Green
        }

        private Dictionary<SkinMaterials, string> dictSkinMaterials = new Dictionary<SkinMaterials, string>()
        {
            {SkinMaterials.Blue, "MushroomMonBlue" },
            {SkinMaterials.Red, "MushroomRed" },
            {SkinMaterials.Green, "MushroomMonGreen" }
        };

        public enum Animations
        {
            Idle,
            Run,
            Attack,
            Damage,
            Death
        }

        private Dictionary<Animations, string> dictAnimations = new Dictionary<Animations, string>()
        {
            {Animations.Idle, "Idle" },
            {Animations.Run, "Run" },
            {Animations.Attack, "Attack" },
            {Animations.Damage, "Damage" },
            {Animations.Death, "Death" }
        };

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
