namespace Patterns
{
    public interface IClickable
    {
        public void AddClicker(IClick obj);
        public void RemoveClicker(IClick obj);
        public void UpdateClicker();
    }
}
