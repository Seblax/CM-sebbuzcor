public interface IPausable
{
    bool IsPaused { get; }
    void SetPaused(bool isPaused);
}