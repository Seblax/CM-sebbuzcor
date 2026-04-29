using System.Linq;
using UnityEngine;

public class PausableManager : Singleton<PausableManager>
{
    public void GlobalPause(bool pauseState)
    {
        // Buscamos todos los MonoBehaviours que implementen IPausable
        var pausables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                        .OfType<IPausable>();

        foreach (var p in pausables)
        {
            p.SetPaused(pauseState);
        }
    }
}
