using UnityEngine;

public class UIRescaler : MonoBehaviour
{
    public Camera mainCamera;

    void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        SetRectWidthToScreen();
    }

    void SetRectWidthToScreen()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 1. Obtener el ancho de la pantalla en unidades del mundo (World Units)
        float screenHeightWorld = mainCamera.orthographicSize * 2.0f;
        float screenWidthWorld = screenHeightWorld * mainCamera.aspect;

        // 2. Considerar la escala actual del objeto
        // Si el scale es 1, el ancho del rect será igual al ancho del mundo.
        // Si el scale es 0.01, el ancho del rect será 100 veces el ancho del mundo.
        float worldToCanvasScale = transform.localScale.x;

        if (worldToCanvasScale != 0)
        {
            float targetWidth = screenWidthWorld / worldToCanvasScale;

            // 3. Aplicar el nuevo ancho preservando el alto original
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        }
    }
}

