using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Gamemanager;
using ShakeAnimation;
using UnityEngine;

namespace Minigame.Game2
{
    public class AlanAnimation : MonoBehaviour
    {
        private enum AlanState { None, Dead, Terrified, Scared, Normal, Happy }
        private AlanState currentState = AlanState.None;

        private readonly string ALAN_SCRIPTABLEOBJECTS_PATH = "ScriptableObjects/Minigames/Game 2 ScriptableObjects";

        Alan alan;

        SpriteRenderer alanSprite;

        AlanScriptableObject alanObject;

        Shake shake;
        private float interval = 0.05f;

        ParticleSystem particle;

        private void Awake() // Cambiado de Start a Awake
        {
            alan = GetComponentInParent<Alan>();
            alanSprite = GetComponent<SpriteRenderer>();
            particle = GetComponentInChildren<ParticleSystem>();
            shake = GetComponent<Shake>();

            // Es mejor cargar esto una sola vez
            alanObject = Resources.LoadAll<AlanScriptableObject>(ALAN_SCRIPTABLEOBJECTS_PATH).GetRandom();
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

            if (percent <= 0) newState = AlanState.Dead;
            else if (percent < 0.25f) newState = AlanState.Terrified;
            else if (percent < 0.50f) newState = AlanState.Scared;
            else if (percent < 0.75f) newState = AlanState.Normal;
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
            shake.Play(0, 0, 5);
            alanSprite.sprite = alanObject.happy;
        }

        private void Normal()
        {
            shake.Play(2, interval, 5);
            alanSprite.sprite = alanObject.normal;
        }

        private void Scared()
        {
            shake.Play(5f, interval, 5);
            alanSprite.sprite = alanObject.scared;
        }

        private void Terrified()
        {
            shake.Play(10f, interval, 5);
            alanSprite.sprite = alanObject.terrified;
        }

        private void Dead()
        {
            if (particle != null) particle.Play();
            alanSprite.enabled = false;
            AudioManager.instance.PlayEffect("BalloonExplode");

        }
    }
}