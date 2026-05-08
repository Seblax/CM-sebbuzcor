using ShakeAnimation;
using UnityEngine;

namespace Minigame.Game2
{
    public class Block : MonoBehaviour
    {
        public float health = 90;

        private Rigidbody2D rb;
        public Vector3 direction;
        public float impulseForce = 10f;
        public float impulseRange = 10f;

        private Shake shake;
        [Header("Shake Configuration")]
        public float shakeInterval = 2;
        public float shakeMagnitude = 0.2f;

        BlockBehaviour blockBehaviour;

        public AudioClip[] hitSound;

        void Awake()
        {
            shake = GetComponent<Shake>();
            blockBehaviour = GetComponentInParent<BlockBehaviour>();
        }

        void Start()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            blockBehaviour.OnDamageTaken += TakeDamage;
        }

        public void TakeDamage(float currentHealth)
        {
            if (currentHealth >= health && currentHealth <= health + 20)
            {
                shake.Play(currentHealth / 100 * shakeInterval, (currentHealth / 100) * shakeMagnitude);
                AudioManager.instance.PlayEffect(hitSound[Random.Range(0, hitSound.Length)]).GetAudioSource.volume = 0.75f;
            }
            else if (currentHealth < health)
            {
                ImpulsarObjeto();
                Destroy(gameObject, 2f);
            }
        }

        public void ImpulsarObjeto()
        {
            direction = new Vector3(Random.Range(-impulseRange, impulseRange), Random.Range(0.5f, 1f), Random.Range(impulseRange, impulseRange));

            rb.AddForce(direction.normalized * impulseForce, ForceMode2D.Impulse);

            rb.gravityScale = 1;
        }

        private void OnDestroy()
        {
            blockBehaviour.OnDamageTaken -= TakeDamage;
        }
    }
}