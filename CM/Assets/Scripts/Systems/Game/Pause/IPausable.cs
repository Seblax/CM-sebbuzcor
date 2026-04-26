public interface IPausable
{
    bool IsPaused { get; set; }
    void SetPaused(bool isPaused);
}