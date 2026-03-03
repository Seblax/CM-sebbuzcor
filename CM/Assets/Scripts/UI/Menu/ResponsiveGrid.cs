using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Este script redimensiona las celdas de un GridLAyoutGroup 
/// automáticamente, respecto al canvas en el Parent.
/// </summary>
public class ResponsiveGrid : MonoBehaviour
{
    [Header("Settings")]
    public bool width;      // Redimensiona en función del width
    public bool height;     // Redimensiona en función del heiht

    private GridLayoutGroup _grid;
    private RectTransform _canvasRect;    

    void Start()    // --Tiene que ser en el Start, el canvas no se redimensiona post Awake
    {
        _grid = this.gameObject.GetComponent<GridLayoutGroup>();
        _canvasRect = this.gameObject.GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>();

        if (width)
        {
            _grid.cellSize = new Vector2(_canvasRect.rect.width, _grid.cellSize.y);
        }

        if (height)
        {
            _grid.cellSize = new Vector2(_grid.cellSize.x, _canvasRect.rect.height);
        }
    }
}
