using UnityEngine;

public class HomeMenu : Menu
{
    public static float speed = 5000f;
    [Header("Settings")]
    [SerializeField] private int yPosition;

    [Header("References")]
    [SerializeField] private GameObject grid;

    private RectTransform gridRect;

    private float startY;
    private float targetY;

    private void Awake()
    {
        if (grid != null)
            gridRect = grid.GetComponent<RectTransform>();
        else
            Debug.LogError("Grid reference is missing!");
    }

    private void OnEnable()
    {
        startY = gridRect.localPosition.y;
        targetY = yPosition;
    }

    private void Update()
    {
        if (gridRect == null) return;


        if (Mathf.Approximately(gridRect.localPosition.y, yPosition))
        {
            return;
        }

        float currentY = gridRect.localPosition.y;
        float progress = Mathf.Clamp01(Mathf.Abs((currentY - startY) / (targetY - startY)));

        MoveTowards(new Vector3(0, yPosition, 0), speed * Mathf.Max(0.10f, 1 - progress));
    }

    private void MoveTowards(Vector3 target, float moveSpeed)
    {
        // Se multiplica por deltaTime para que el movimiento sea consistente
        gridRect.localPosition = Vector3.MoveTowards(gridRect.localPosition, target, moveSpeed * Time.deltaTime);
    }
}
