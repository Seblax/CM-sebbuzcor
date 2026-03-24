namespace Player
{
    public interface IClickHoldable : IClickable
    {
        void OnHold();
        void OnRelease();
    }
}