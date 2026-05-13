using UnityEngine;

namespace Minigame.Game3
{
    public class LanternBeamLight : MonoBehaviour
    {
        void Start()
        {
            var lantern = GetComponentInParent<Lantern>();
            if (lantern != null)
                lantern.OnPositionChange += UpdateLightBeam;
        }

        void UpdateLightBeam(Vector3 newPosition)
        {
            float y = newPosition.y;

            float finalScaleY = Data.Minigame.Game3.Lantern.Beam.BASE_SCALE + (y * Data.Minigame.Game3.Lantern.Beam.SCALE_MODIFIER);

            // Calculamos la nueva posición local
            float finalPosY = Data.Minigame.Game3.Lantern.Beam.BASE_POSITION + (y * Data.Minigame.Game3.Lantern.Beam.POSITION_MODIFIER);

            transform.localScale = new Vector3(transform.localScale.x, finalScaleY, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, finalPosY, transform.localPosition.z);
        }
    }
}