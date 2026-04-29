using Minigame;
using System.Collections;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Vector3 direction = Vector3.up; // Dirección del movimiento
    public float distance = 10f;             // A + 10
    public float duration = 0.25f;           // Tiempo en segundos

    // Propiedad para consultar si ha terminado desde fuera
    public bool IsMovementComplete => !MinigameManager.instance.isMoving;

    private void Awake()
    {
        MinigameManager.instance.Move += StartMove;
    }

    private void OnDestroy()
    {
        MinigameManager.instance.Move -= StartMove;
    }

    [ContextMenu("Ejecutar Movimiento")] // Permite probarlo desde el Inspector
    public void StartMove()
    {
        if (!MinigameManager.instance.isMoving)
        {
            StartCoroutine(MoveRoutine(transform.position, transform.position + (direction.normalized * distance), duration));
        }
    }

    private IEnumerator MoveRoutine(Vector3 startPos, Vector3 endPos, float time)
    {
        MinigameManager.instance.isMoving = true;
        float elapsed = 0f;

        while (elapsed < time)
        {
            // Calculamos el progreso entre 0 y 1
            float t = elapsed / time;

            // Interpolar posición de forma lineal
            transform.position = Vector3.Lerp(startPos, endPos, t);

            elapsed += Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        // Aseguramos la posición final exacta
        transform.position = endPos;
        MinigameManager.instance.isMoving = false;
    }
}