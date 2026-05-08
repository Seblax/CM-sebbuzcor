using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using UnityEngine;

namespace Minigame.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private string[] scoreText = { "Null" };

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = scoreText.GetRandom();
        }
    }
}