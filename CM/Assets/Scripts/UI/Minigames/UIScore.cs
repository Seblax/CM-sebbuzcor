using UnityEngine;

namespace Minigame.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] GameObject victory;
        [SerializeField] GameObject defeat;

        [SerializeField] UIUser user;
        [SerializeField] UserScriptableObject userData;

        void Awake()
        {
            MinigameUIManager.instance.OnLivesChanged += UpdateScoreUI;
            user = GetComponentInChildren<UIUser>();

            userData.userComment = $"Your current score: {GameManager.instance.score}";

        }

        void UpdateScoreUI(int i, bool animation)
        {
            MinigameUIManager.instance.OnLivesChanged -= UpdateScoreUI;

            this.victory.SetActive(MinigameManager.instance.minigame.Win);
            this.defeat.SetActive(MinigameManager.instance.minigame.Lose);
            userData.userComment = $"Your current score: {GameManager.instance.score}";
            user.UpdateUI(userData);
        }


    }
}
