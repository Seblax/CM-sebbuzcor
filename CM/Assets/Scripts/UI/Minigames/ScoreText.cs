using EasyTextEffects;
using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using UnityEngine;

namespace Minigame.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private string[] scoreText = { "Null" };

        void Awake()
        {
            this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = scoreText.GetRandom();
            this.GetComponentInChildren<TextEffect>().Refresh();
        }
    }
}