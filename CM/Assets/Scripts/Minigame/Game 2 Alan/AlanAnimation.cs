using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Animation;
using UnityEngine;

namespace Minigame.Game2
{
    public class AlanAnimation : MonoBehaviour
    {
        private enum AlanState { None, Dead, Terrified, Scared, Normal, Happy }
        private AlanState currentState = AlanState.None;

        Alan alan;
        SpriteRenderer alanSprite;
        AlanScriptableObject alanObject;

        Shake shake;
        
        ParticleSystem particle;

        private void Awake() // Cambiado de Start a Awake
        {
            alan = GetComponentInParent<Alan>();
            alanSprite = GetComponent<SpriteRenderer>();
            particle = GetComponentInChildren<ParticleSystem>();
            shake = GetComponent<Shake>();

            // Es mejor cargar esto una sola vez
            alanObject = Resources.LoadAll<AlanScriptableObject>(Data.Minigame.Game2.Alan.ALAN_SCRIPTABLEOBJECTS_PATH).GetRandom();
        }

        private void OnEnable()
        {
            if (alan != null)
                alan.OnAlanChange += UpdateSprite;
        }

        private void OnDisable()
        {
            if (alan != null)
                alan.OnAlanChange -= UpdateSprite;
        }

        private void UpdateSprite(float percent)
        {
            AlanState newState;

            if (percent <= Data.Minigame.Game2.Alan.Dead.PERCENT) newState = AlanState.Dead;
            else if (percent < Data.Minigame.Game2.Alan.Terrified.PERCENT) newState = AlanState.Terrified;
            else if (percent < Data.Minigame.Game2.Alan.Scared.PERCENT) newState = AlanState.Scared;
            else if (percent < Data.Minigame.Game2.Alan.Normal.PERCENT) newState = AlanState.Normal;
            else newState = AlanState.Happy;

            if (newState != currentState)
            {
                currentState = newState;

                switch (currentState)
                {
                    case AlanState.Dead: Dead(); break;
                    case AlanState.Terrified: Terrified(); break;
                    case AlanState.Scared: Scared(); break;
                    case AlanState.Normal: Normal(); break;
                    case AlanState.Happy: Happy(); break;
                }
            }
        }

        private void Happy()
        {
            shake.Play(
                Data.Minigame.Game2.Alan.Happy.SHAKE_SPEED,
                Data.Minigame.Game2.Alan.Shake.INTERVAL,
                Data.Minigame.Game2.Alan.Shake.DURATION);
            alanSprite.sprite = alanObject.normal;
        }

        private void Normal()
        {
            shake.Play(
                Data.Minigame.Game2.Alan.Normal.SHAKE_SPEED, 
                Data.Minigame.Game2.Alan.Shake.INTERVAL,
                Data.Minigame.Game2.Alan.Shake.DURATION);
            alanSprite.sprite = alanObject.normal;
        }

        private void Scared()
        {
            shake.Play(
                Data.Minigame.Game2.Alan.Scared.SHAKE_SPEED,
                Data.Minigame.Game2.Alan.Shake.INTERVAL,
                Data.Minigame.Game2.Alan.Shake.DURATION);
            alanSprite.sprite = alanObject.normal;
        }

        private void Terrified()
        {
            shake.Play(
                Data.Minigame.Game2.Alan.Terrified.SHAKE_SPEED,
                Data.Minigame.Game2.Alan.Shake.INTERVAL,
                Data.Minigame.Game2.Alan.Shake.DURATION);
            alanSprite.sprite = alanObject.normal;
        }

        private void Dead()
        {
            if (particle != null) particle.Play();
            alanSprite.enabled = false;
            AudioManager.instance.PlayEffect(Data.Minigame.Game2.Alan.BALLOON_EXPLODE_SOUND);
        }
    }
}