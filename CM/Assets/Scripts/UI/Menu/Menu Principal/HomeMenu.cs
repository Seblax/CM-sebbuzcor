using System.Collections;
using UnityEngine;

/// <summary>
/// Script que se encarga de la lógica de los menus principales del juego. Esta clase es heredada de Menu
/// la cual gestion al lógica interna de los menús. Este script se centra en la animación y el comportamiento
/// de los menús.
/// </summary>
/// 
public class HomeMenu : IMenu
{
    public static float speed = 100f;                   // Velocidad de la animación

    [Header("Settings")]
    public int yPosition;                               // Posición 
    public float delay = 1f;                                 // Tiempo que tarda en desaparecer el menú
    [Header("References")]
    [SerializeField] private float _startY;
    [SerializeField] private float _targetY;
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _menuObject;

    private RectTransform _gridRect;

    Coroutine _disableCoroutine;

    private void Awake()
    {
        _menuObject = this.transform.GetChild(0).gameObject;

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
        if (_gridRect == null) return;                                              // Si no hay grid, no se ejecuta nada
        if (Mathf.Approximately(_gridRect.localPosition.y, yPosition)) return;      // Si está cerca del objetivo, no se ejecuta nada
        if (Mathf.Approximately(_startY, _targetY)) return;                         

        float currentY = _gridRect.localPosition.y;
        float progress = Mathf.Clamp01(Mathf.Abs((currentY - _startY) / (_targetY - _startY))); //Progreso porcentual según la distancia, inicialmente rápido, y se va desacelerando

        MoveTowards(new Vector3(0, yPosition, 0), speed * Mathf.Max(0.10f, 1 - progress));
    }

    /// <summary>
    /// Lógica del movimiento de la grid del Parent
    /// </summary>
    /// <param name="target"></param>
    /// <param name="moveSpeed"></param>
    private void MoveTowards(Vector3 target, float moveSpeed)
    {
        _gridRect.localPosition = Vector3.MoveTowards(_gridRect.localPosition, target, moveSpeed);
    }

    /// <summary>
    /// Se activa el menú visualmente
    /// </summary>
    public void Enable()
    {
        _menuObject.SetActive(true);

        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);

        _startY = _gridRect.localPosition.y;
        _targetY = yPosition;
        this.enabled = true;
    }

    /// <summary>
    /// Se desactiva el menú visualmente
    /// </summary>
    public void Disable()
    {
        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);

        _disableCoroutine = StartCoroutine(DisableAfterDelay(delay));
        this.enabled = false;
    }

    /// <summary>
    /// Coroutina para desactivar el menú con delay
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _menuObject.SetActive(false);
    }
}
