using UnityEngine;

namespace Minigame.Game0
{
    public class Signal : MonoBehaviour
    {
        public float percentActivation = 0f;

        CarBehaviour car;
        SpriteRenderer spriteRenderer;
        float timer;
        bool play;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            car = FindAnyObjectByType<CarBehaviour>().GetComponent<CarBehaviour>();

            spriteRenderer.enabled = false;
            play = false;
            timer = Data.Minigame.Game0.Signal.SHOW_TIME;
        }

        private void OnEnable()
        {
            car.ShowSignal += ShowSignal;
        }

        private void OnDisable()
        {
            car.ShowSignal -= ShowSignal;
        }

        void ShowSignal(float percent)
        {
            if (percent <= percentActivation || play) return;
            AudioManager.instance.PlayEffect(Data.Minigame.Game0.Signal.ALARM_SOUND);
            play = true;
        }

        private void Update()
        {
            if (!play) return;
            timer -= Time.deltaTime;
            spriteRenderer.enabled = timer > 0;

            if (timer <= 0) {
                Destroy(this);
            }
        }
    }
}