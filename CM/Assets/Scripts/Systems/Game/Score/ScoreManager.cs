using System;
using UnityEngine;

namespace Score
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public Action UpdateLeaderBoard;

        public void UpdateLeaderBoardData()
        {
            UpdateLeaderBoard?.Invoke();
        }
    }
}