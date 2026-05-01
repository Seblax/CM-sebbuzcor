using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Minigame;
using System.Collections;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    SpriteRenderer[] _sprenderers;
    [SerializeField] Sprite[] cars;
    [SerializeField] Sprite wheels;

    ConstantMove mover;
    Hop carHop;

    public float distance = 100f;
    public float interval = 5f;
    float totalWindow;

    private void Start()
    {
        this.transform.localPosition += Vector3.right* (distance / 2);
        mover = GetComponent<ConstantMove>();
        _sprenderers = GetComponentsInChildren<SpriteRenderer>();
        carHop = GetComponentInChildren<Hop>();

        mover.direction = Vector3.left;
        mover.distance = distance;
        mover.duration = interval;
        totalWindow = MinigameManager.instance.minigame.GetMinigameDuration;

        SetSkin();
        StartCoroutine(SpawningLoop());
    }

    private IEnumerator SpawningLoop()
    {
        while (true)
        {
            // 1. Calculamos el tiempo de espera aleatorio
            // Restamos la duración del movimiento para no pasarnos de los 7s totales
            float moveDuration = mover.duration;
            float maxWait = totalWindow - moveDuration;
            float randomWait = Random.Range(0f, maxWait);

            // 2. Esperamos el tiempo aleatorio
            yield return new WaitForSeconds(randomWait);

            // 3. Ejecutamos el movimiento
            mover.StartMove();
            Destroy(this.gameObject);
        }
    }

    void SetSkin() {
        _sprenderers[0].sprite = cars.GetRandom();
        _sprenderers[1].sprite = wheels;
        _sprenderers[2].sprite = wheels;
    }

    private void Update()
    {
        if(!carHop.IsHopping) carHop.Play();
    }
}
