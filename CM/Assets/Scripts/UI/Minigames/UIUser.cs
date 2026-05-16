using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.UI
{

    public class UIUser : MonoBehaviour
    {
        [Header("User components")]
        public Image userImage;
        public TextMeshProUGUI userName;
        public TextMeshProUGUI userComment;

        [Header("Post components")]
        public TextMeshProUGUI postLikes;
        public TextMeshProUGUI postComments;

        void Awake()
        {
            MinigameUIManager.instance.OnUserChanged += UpdateUI;
        }

        public void UpdateUI(UserScriptableObject userData)
        {
            if (!this.gameObject.activeInHierarchy) return;
            MinigameUIManager.instance.OnUserChanged -= UpdateUI;

            SetUserUI(userData);
            SetPostUI(userData);
        }

        void SetUserUI(UserScriptableObject userData)
        {
            userImage.sprite = userData.userPicture;
            userName.text = userData.userName;
            userComment.text = userData.userComment;
        }

        void SetPostUI(UserScriptableObject userData)
        {
            SetTextSize(postLikes);
            SetTextSize(postComments);

            postLikes.text = userData.FormatQuantity(userData.likes);
            postComments.text = userData.FormatQuantity(userData.comments);
        }

        void SetTextSize(TextMeshProUGUI text)
        {
            text.enableAutoSizing = true;
            text.fontSizeMin = 18f;
            text.fontSizeMax = 36f;
        }
    }

}