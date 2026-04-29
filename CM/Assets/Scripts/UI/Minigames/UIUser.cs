using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
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

        void UpdateUI(UserScriptableObject userData)
        {
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
            postLikes.text = userData.FormatQuantity(userData.likes);
            postComments.text = userData.FormatQuantity(userData.comments);
        }
    }

}