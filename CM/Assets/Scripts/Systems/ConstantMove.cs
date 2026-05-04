using Minigame;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class ConstantMove : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Vector3 direction = Vector3.up; // Dirección del movimiento
    public float distance = 10f;             // A + 10
    public float duration = 0.25f;           // Tiempo en segundos
    private float percent;           // Tiempo en segundos

    public float Percent { get => percent; }



    public bool isMoving = false; // Variable para controlar el estado del movimiento

    // Propiedad para consultar si ha terminado desde fuera
    public bool IsMovementComplete => !isMoving;


    [ContextMenu("Ejecutar Movimiento")] // Permite probarlo desde el Inspector
    public void StartMove()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveRoutine(transform.localPosition, transform.localPosition + (direction.normalized * distance), duration));
        }
    }

    private IEnumerator MoveRoutine(Vector3 startPos, Vector3 endPos, float time)
    {
        isMoving = true;
        float elapsed = 0f;

        while (elapsed < time)
        {
            // Calculamos el progreso entre 0 y 1
            float t = elapsed / time;

            // Interpolar posición de forma lineal
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            elapsed += Time.deltaTime;
            yield return null; // Espera al siguiente frame

            percent = Mathf.Clamp01(elapsed / time); // Actualiza el porcentaje de movimiento
        }

        // Aseguramos la posición final exacta
        transform.localPosition = endPos;
        isMoving = false;
    }
}