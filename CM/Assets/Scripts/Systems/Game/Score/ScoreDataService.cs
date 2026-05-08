using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Score
{
    public class ScoreDataService : MonoBehaviour
    {
        private static string[] ScoreKeys = new string[]
        {
            "UserScore_1",
            "UserScore_2",
            "UserScore_3",
            "UserScore_4",
            "UserScore_5",
        };

        public static List<Tuple<string, int>> LoadScore()
        {
            List<Tuple<string, int>> res = new List<Tuple<string, int>>();

            foreach (var key in ScoreKeys)
            {
                res.Add(new Tuple<string, int>(key,PlayerPrefs.GetInt(key, 0)));
            };

            res.Sort((a, b) => b.Item2.CompareTo(a.Item2));

            return res;
        }

        public static void SaveScore(int currentScore)
        {
            string key = LoadScore().First(x => (x.Item2 <= currentScore)).Item1;

            PlayerPrefs.SetInt(key, currentScore);
            PlayerPrefs.Save();

            Debug.Log("Puntuación guardada: " + currentScore);
        }

        public static void DeleteScore()
        {

            foreach (var key in ScoreKeys)
            {
                PlayerPrefs.SetInt(key, 0);
            }
            ;
        }
    }
}