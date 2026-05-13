using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Minigame.Game3;
using ShakeAnimation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Minigame.Game3
{
    public class Tomb : MonoBehaviour
    {
        private static string TOMB_SPRITE_PATH = "Textures/Minigame/Game 3/tombs";
        private Cat _cat;

        private SpriteRenderer _spriteRenderer;
        private Sprite[] _tombSprites;

        private Shake _shake;

        void Start()
        {
            _tombSprites = Resources.LoadAll<Sprite>(TOMB_SPRITE_PATH);
            
            _shake = GetComponent<Shake>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            this.transform.localScale *= Random.Range(0.80f, 2.5f);

            _spriteRenderer.sprite = _tombSprites.GetRandom();
        }

        void OnEnable() // Cambiado de Start a OnEnable
        {
            _cat = Object.FindAnyObjectByType<Cat>();

            if (_cat != null)
            {
                // Limpiamos primero para evitar duplicados y luego suscribimos
                _cat.UpdateTombSpriteLayer -= SetTombSpriteLayer;
                _cat.UpdateTombSpriteLayer += SetTombSpriteLayer;
            }
        }

        private void OnDisable()
        {
            if (_cat != null)
            {
                // SIEMPRE desuscribirse con -=
                _cat.UpdateTombSpriteLayer -= SetTombSpriteLayer;
            }
        }

        void SetTombSpriteLayer(float catY)
        {
            _shake.Play(5, 0.015f, 10f);

            float y = transform.position.y;
            int sortingLayer = y > catY ? -1 : 1;

            this._spriteRenderer.sortingOrder = sortingLayer;
        }
    }

}