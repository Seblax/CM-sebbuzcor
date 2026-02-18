using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomeMenu : IMenu
{
    public static float speed = 100f;
    [Header("Settings")]
    [SerializeField] private int yPosition;

    [Header("References")]
    [SerializeField] private GameObject _grid;
    private GameObject _childObject;

    private RectTransform _gridRect;

    private float _startY;
    private float _targetY;

    Coroutine _disableCoroutine;

    private void Awake()
    {
        _childObject = this.transform.GetChild(0).gameObject;

        if (_grid != null)
        {
            _gridRect = _grid.GetComponent<RectTransform>();
            _startY = _gridRect.localPosition.y;
            _targetY = yPosition;
        }
        else
            Debug.LogError("Grid reference is missing!");
    }
    private void Update()
    {
        if (_gridRect == null) return;
        if (Mathf.Approximately(_gridRect.localPosition.y, yPosition)) return;
        if (Mathf.Approximately(_startY, _targetY)) return;

        float currentY = _gridRect.localPosition.y;
        float progress = Mathf.Clamp01(Mathf.Abs((currentY - _startY) / (_targetY - _startY)));

        MoveTowards(new Vector3(0, yPosition, 0), speed * Mathf.Max(0.10f, 1 - progress));
    }

    private void MoveTowards(Vector3 target, float moveSpeed)
    {
        _gridRect.localPosition = Vector3.MoveTowards(_gridRect.localPosition, target, moveSpeed);
    }

    public void Enable()
    {
        _childObject.SetActive(true);

        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);

        _startY = _gridRect.localPosition.y;
        _targetY = yPosition;
        this.enabled = true;
    }

    public void Disable()
    {
        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);

        _disableCoroutine = StartCoroutine(DisableAfterDelay(1f));
        this.enabled = false;
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _childObject.SetActive(false);
    }
}
