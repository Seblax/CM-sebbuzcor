/// <summary>
/// Clase que hereda implementación de Beat. Se diferencia de la clase Beat normal porque esta se
/// reinicia cada vez que la música del menu se loopea.
/// </summary>

public class BeatMenu : Beat
{
    private float _lastTime;    // Comprueba cuanto tiempo lleva la canción en el frame anterior

    new void Update()
    {
        //  Si en el frame actual, la canción se ah reiniciado y por tanto el tiempo actual
        //  es menor que el nuevo, se reinicia Beat
        if (MenuData.MENUMUSIC_TIMER < _lastTime)
        {
            // Ejecutamos el Start de Beat
            base.Start();
        }

        //actualizamos valor de lastTime
        _lastTime = MenuData.MENUMUSIC_TIMER;

        // Ejecutamos el Update de Beat
        base.Update();
    }
}
