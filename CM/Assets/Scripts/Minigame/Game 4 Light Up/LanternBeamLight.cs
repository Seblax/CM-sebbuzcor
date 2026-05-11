using UnityEngine;

namespace Minigame.Game4
{
    public class LanternBeamLight : MonoBehaviour
    {
        [SerializeField] private float baseScaleY = -2.5f;
        [SerializeField] private float basePosY = -0.75f;

        // Definimos cuánto cambia la escala por cada unidad de 'y'
        // Si quieres que crezca cuando 'y' sube, usa un valor positivo.
        [SerializeField] private float scaleModifier = -1.2f;
        [SerializeField] private float posModifier = -0.5f;

        void Start()
        {
            var lantern = GetComponentInParent<Lantern>();
            if (lantern != null)
                lantern.OnPositionChange += UpdateLightBeam;
        }

        void UpdateLightBeam(Vector3 newPosition)
        {
            float y = newPosition.y;

            // Calculamos la nueva escala asegurándonos de que no sea negativa 
            // (a menos que quieras el efecto espejo a propósito)
            float finalScaleY = baseScaleY + (y * scaleModifier);

            // Calculamos la nueva posición local
            float finalPosY = basePosY + (y * posModifier);

            // Aplicamos los cambios
            transform.localScale = new Vector3(transform.localScale.x, finalScaleY, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, finalPosY, transform.localPosition.z);
        }
    }
}