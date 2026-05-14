using Animation;
using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System.Collections;
using UnityEngine;

namespace Minigame.Game0
{
    public class CarBehaviour : MonoBehaviour, IPausable
    {
        SpriteRenderer[] _sprenderers;
        [SerializeField] Sprite[] _cars;
        [SerializeField] Sprite _wheels;

        BoxCollider2D _boxCollider2D;

        ConstantMove _mover;
        Hop _hop;

        private float totalWindow;

        //Pause
        bool _paused = true;
        public bool IsPaused { get => _paused; }

        //Sound
        private Sound moveSound;

        private void Start()
        {
            this.transform.localPosition += Vector3.right * (Data.Minigame.Game0.Car.Mover.DISTANCE / 2);

            _mover = GetComponent<ConstantMove>();
            _sprenderers = GetComponentsInChildren<SpriteRenderer>();
            _hop = GetComponentInChildren<Hop>();
            _boxCollider2D = GetComponent<BoxCollider2D>();

            SetHopConfiguration();
            SetMoverConfiguration();
            SetSkin();

            totalWindow = MinigameManager.instance.minigame.GetMinigameDuration;

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

        public void DisableCarCollider(float x)
        {
            if (x >= this.transform.localPosition.x)
                Destroy(_boxCollider2D);
        }

        private IEnumerator SpawningLoop()
        {
            yield return new WaitUntil(() => !IsPaused);

            AudioManager.instance.PlayEffect(Data.Minigame.Game0.Car.CAR_START_SOUND);

            while (true)
            {

                // 1. Calculamos el tiempo de espera aleatorio
                // Restamos la duración del movimiento para no pasarnos de los 7s totales
                float moveDuration = _mover.duration;
                float maxWait = totalWindow - moveDuration;
                float randomWait = Random.Range(0f, maxWait);


                // 2. Esperamos el tiempo aleatorio
                yield return new WaitForSeconds(randomWait);

                // 3. Ejecutamos el movimiento
                _mover.StartMove();

                if (_mover.IsMovementComplete)
                {
                    Destroy(this.gameObject);
                    Destroy(moveSound.gameObject);
                    SetPaused(true);
                }

            }
        }

        void SetSkin()
        {
            _sprenderers[0].sprite = _cars.GetRandom();
            _sprenderers[1].sprite = _wheels;
            _sprenderers[2].sprite = _wheels;
        }

        private void Update()
        {
            if (IsPaused) return;

            if (!_hop.IsHopping) _hop.Play();

            if (moveSound == null && !_mover.IsMovementComplete) moveSound = AudioManager.instance.PlayEffect(Data.Minigame.Game0.Car.CAR_LOOP_SOUND);

            if (moveSound != null && moveSound.GetAudioSource != null)
            {
                moveSound.GetAudioSource.panStereo = 1f - (_mover.Percent * 2f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Data.Minigame.PLAYER_TAG))
            {
                collision.gameObject.GetComponentInParent<AnimalBehaviour>().Hit();
            }
        }

        public void SetPaused(bool isPaused)
        {
            _paused = isPaused;
        }

        void SetHopConfiguration()
        {
            _hop.amplitude = Data.Minigame.Game0.Car.Hop.AMPLITUDE;
            _hop.speed = Data.Minigame.Game0.Car.Hop.SPEED;
            _hop.direction = Data.Minigame.Game0.Car.Hop.DIRECTION;
        }

        void SetMoverConfiguration()
        {
            _mover.direction = Vector3.left;
            _mover.distance = Data.Minigame.Game0.Car.Mover.DISTANCE;
            _mover.duration = Data.Minigame.Game0.Car.Mover.INTERVAL;
        }
    }
}
