public class BeatMenu : Beat
{
    private float _lastTime;

    new void Update()
    {
        if (MenuData.MENUMUSIC_TIMER < _lastTime)
        {
            base.Start();
        }

        _lastTime = MenuData.MENUMUSIC_TIMER;

        base.Update();
    }
}
