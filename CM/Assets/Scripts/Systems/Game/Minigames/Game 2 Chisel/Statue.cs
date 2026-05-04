using UnityEngine;

namespace Minigame.Game2
{
    public class Statue : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer statueSprite;
        [SerializeField] private Sprite[] statueSprites;
        [SerializeField] private Sprite[] statueSpritesVictory;
        [SerializeField] private int spriteID;

        void Start()
        {
            spriteID = Random.Range(0, statueSprites.Length);
            this.statueSprite = GetComponentInChildren<SpriteRenderer>();

            this.statueSprite.sprite = statueSprites[spriteID];
        }

        private void Update()
        {
            if (MinigameManager.instance.minigame.Win)
            {
                this.statueSprite.sprite = statueSpritesVictory[spriteID];
            }
        }
    }
}