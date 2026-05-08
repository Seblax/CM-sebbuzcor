using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Minigame;
using System.Collections;
using UnityEngine;

namespace Game0
{
    public class CarBehaviour : MonoBehaviour, IPausable
    {
        SpriteRenderer[] _sprenderers;
        [SerializeField] Sprite[] cars;
        [SerializeField] Sprite wheels;

        ConstantMove mover;
        Hop carHop;

        public float distance = 100f;
        public float interval = 5f;
        float totalWindow;

        bool paused = true;
        public bool IsPaused { get => paused; }

        private Sound moveSound;

        private void Start()
        {
            this.transform.localPosition += Vector3.right * (distance / 2);
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

        private void OnEnable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        private void OnDisable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }

        private IEnumerator SpawningLoop()
        {
            yield return new WaitUntil(() => !IsPaused);

            AudioManager.instance.PlayEffect("CarStart");

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

                if (mover.IsMovementComplete)
                {
                    Destroy(this.gameObject);
                    Destroy(moveSound.gameObject);
                    SetPaused(true);
                }

            }
        }

        void SetSkin()
        {
            _sprenderers[0].sprite = cars.GetRandom();
            _sprenderers[1].sprite = wheels;
            _sprenderers[2].sprite = wheels;
        }

        private void Update()
        {
            if (IsPaused) return;

            if (!carHop.IsHopping) carHop.Play();

            if (moveSound == null && !mover.IsMovementComplete) moveSound = AudioManager.instance.PlayEffect("CarMoveLoop");

            if (moveSound != null && moveSound.GetAudioSource != null)
            {
                moveSound.GetAudioSource.panStereo = 1f - (mover.Percent * 2f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponentInParent<AnimalBehaviour>().Hit();
            }
        }

        public void SetPaused(bool isPaused)
        {
            paused = isPaused;
        }
    }
}
