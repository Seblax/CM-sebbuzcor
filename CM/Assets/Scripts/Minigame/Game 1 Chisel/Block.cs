using ShakeAnimation;
using UnityEngine;

namespace Minigame.Game1
{
    public class Block : MonoBehaviour
    {
        public float health = 90;

        private Rigidbody2D rb;
        public Vector3 direction;

        private Shake shake;

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
                float percent = currentHealth / 100;

                shake.Play(percent * Data.Minigame.Game1.Blcok.Shake.INTERVAL, percent * Data.Minigame.Game1.Blcok.Shake.MAGNITUDE);
                AudioManager.instance.PlayEffect(hitSound[Random.Range(0, hitSound.Length)]).GetAudioSource.volume = 0.75f;
            }
            else if (currentHealth < health)
            {
                ImpulsarObjeto();
                Destroy(gameObject, Data.Minigame.Game1.Blcok.DESTROY_DELAY);
            }
        }

        public void ImpulsarObjeto()
        {
            float xRange = Random.Range(-Data.Minigame.Game1.Blcok.Impulse.X_RANGE, Data.Minigame.Game1.Blcok.Impulse.X_RANGE);
            float zRange = 0;

            direction = new Vector3(xRange, Data.Minigame.Game1.Blcok.Impulse.Y_RANGE, zRange);

            rb.AddForce(direction.normalized * Data.Minigame.Game1.Blcok.Impulse.FORCE, ForceMode2D.Impulse);

            rb.gravityScale = 1;
        }

        private void OnDestroy()
        {
            blockBehaviour.OnDamageTaken -= TakeDamage;
        }
    }
}