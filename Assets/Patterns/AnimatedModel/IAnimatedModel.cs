namespace Patterns
{
    /// <summary>
    /// Шаблон для объектов моделей, использующих анимации, для формирования карты данных анимации
    /// </summary>
    public interface IAnimatedModel<T, K>
    {
        public string Name { get; }
        public string Res_Path { get; }

        public enum SkinMaterials { };
        public enum Animations { };

        public string SkinMaterial(T value);
        public string Animation(K value);
    }
}
