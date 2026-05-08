using EasyTextEffects;
using TMPro;
using UnityEngine;

namespace Minigame.UI
{
    public class UITittle : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI tittleText;
        [SerializeField]
        TextMeshProUGUI tittleRound;
        TextEffect tittleEffect;

        private void Awake()
        {
            tittleEffect = tittleText.GetComponentInChildren<TextEffect>();
            MinigameUIManager.instance.OnTittleChanged += UpdateUI;
        }

        void UpdateUI(TittleScriptableObject tittle)
        {
            this.tittleText.text = tittle.tittle;
            this.tittleRound.text = "Round: " + GameManager.instance.GetCurrentRound;

            this.tittleEffect.Refresh();
            MinigameUIManager.instance.UpdateUserUI(tittle.GetAdUser());
        }
    }
}