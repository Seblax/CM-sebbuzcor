using Animation;
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

                float interval = percent * Random.Range(Data.Minigame.Game1.Blcok.Shake.INTERVAL.Item1, Data.Minigame.Game1.Blcok.Shake.INTERVAL.Item2);
                float magnitude = percent * Random.Range(Data.Minigame.Game1.Blcok.Shake.MAGNITUDE.Item1, Data.Minigame.Game1.Blcok.Shake.MAGNITUDE.Item2);

                shake.Play(interval, magnitude);

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
            float xRange = Random.Range(Data.Minigame.Game1.Blcok.Impulse.X_RANGE.Item1, Data.Minigame.Game1.Blcok.Impulse.X_RANGE.Item2);
            float force = Random.Range(Data.Minigame.Game1.Blcok.Impulse.FORCE.Item1, Data.Minigame.Game1.Blcok.Impulse.FORCE.Item2);

            float x = Random.Range(-xRange, xRange);
            float y = Random.Range(Data.Minigame.Game1.Blcok.Impulse.Y_RANGE.Item1, Data.Minigame.Game1.Blcok.Impulse.Y_RANGE.Item2);
            float z = 0;

            direction = new Vector3(x, y, z);

            rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);

            rb.gravityScale = 5f;
        }

        private void OnDestroy()
        {
            blockBehaviour.OnDamageTaken -= TakeDamage;
        }
    }
}