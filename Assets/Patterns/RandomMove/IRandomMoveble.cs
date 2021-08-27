namespace Patterns
{
    public interface IRandomMoveble
    {
        /// <summary>
        /// Добавление в список IRandomMove
        /// </summary>
        public void AddRandomMove(IRandomMove move);

        /// <summary>
        /// Удаление из списка IRandomMove при удалении этого объекта
        /// </summary>
        public void RemoveRandomMove(IRandomMove move);

        /// <summary>
        /// Проверка статуса движения IRandomMove
        /// </summary>
        public void UpdateRandomMove();
    }
}
