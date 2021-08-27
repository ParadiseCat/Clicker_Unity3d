namespace Patterns
{
    /// <summary>
    /// Шаблон управления обновлений для главного менеджера
    /// </summary>
    public interface IUpdateManager
    {
        public void SetUpdateBehaviour(IUpdate obj);
        public void ResetUpdateBehaviour(IUpdate obj);
    }
}
