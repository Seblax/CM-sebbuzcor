using TMPro;
using UnityEngine;

namespace Minigame
{
    public class UITittle : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI tittleText;
        [SerializeField]
        TextMeshProUGUI tittleRound;

        private void Awake()
        {
            MinigameUIManager.instance.OnTittleChanged += UpdateUI;    
        }

        void UpdateUI(TittleScriptableObject tittle)
        {
            this.tittleText.text = tittle.tittle;
            this.tittleRound.text = "Round: " + GameManager.instance.GetCurrentRound;

            MinigameUIManager.instance.UpdateUserUI(tittle.adUser);
        }
    }
}