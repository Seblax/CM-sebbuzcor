using TMPro;
using UnityEngine;

namespace Score
{
    public class LeaderboardSlotDisplay : MonoBehaviour
    {
        public int index;
        private TextMeshProUGUI text;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            ScoreManager.instance.UpdateLeaderBoard += UpdateScore;
        }

        private void OnDisable()
        {
            ScoreManager.instance.UpdateLeaderBoard -= UpdateScore;
        }

        void UpdateScore()
        {
            text.text = (ScoreDataService.LoadScore().Find(x => x.Item1 == $"UserScore_{index}").Item2).ToString();
        }
    }

}