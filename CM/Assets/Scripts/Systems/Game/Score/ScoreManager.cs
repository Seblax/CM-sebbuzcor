using System;
using UnityEngine;

namespace Score
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public Action UpdateLeaderBoard;

        void Start()
        {
            UpdateLeaderBoard.Invoke();
        }

        public void UpdateLeaderBoardData()
        {
            UpdateLeaderBoard.Invoke();
        }
    }
}