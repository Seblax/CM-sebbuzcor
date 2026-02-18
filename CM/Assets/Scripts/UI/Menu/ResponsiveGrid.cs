using UnityEngine;
using UnityEngine.UI;

public class ResponsiveGrid : MonoBehaviour
{
    [Header("Settings")]
    public bool width;
    public bool height;

    private GridLayoutGroup _grid;
    private RectTransform _rect;

    void Start()
    {
        _grid = this.gameObject.GetComponent<GridLayoutGroup>();
        _rect = this.gameObject.GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>();

        if (width)
        {
            _grid.cellSize = new Vector2(_rect.rect.width, _grid.cellSize.y);
        }

        if (height)
        {
            _grid.cellSize = new Vector2(_grid.cellSize.x, _rect.rect.height);
        }
    }
}
