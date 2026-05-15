using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System;
using UnityEngine;

namespace Minigame
{
    public class MinigameUIManager : Singleton<MinigameUIManager>
    {
        public readonly string MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH = "ScriptableObjects/Minigames/UI/User/";
        public readonly string MINIGAME_TITTLE_UI_SCRIPTABLEOBJECT_PATH = "ScriptableObjects/Minigames/UI/Tittle/";

        UserScriptableObject[] userUIData;

        public Action<float> OnHealthBarChanged;
        public Action<TittleScriptableObject> OnTittleChanged;
        public Action<UserScriptableObject> OnUserChanged;
        public Action<int, bool> OnLivesChanged;

        public GameObject tittle;
        public GameObject minigame;
        public GameObject score;

        public GameObject Tittle
        {
            get
            {
                if (tittle == null)
                {
                    tittle = MinigameManager.instance.minigame.tittle;
                }
                return tittle;
            }
        }

        public GameObject Minigame
        {
            get
            {
                if (tittle == null)
                {
                    tittle = MinigameManager.instance.minigame.game;
                }
                return tittle;
            }
        }

        public GameObject Score
        {
            get
            {
                if (tittle == null)
                {
                    tittle = MinigameManager.instance.minigame.score;
                }
                return tittle;
            }
        }

        private void Awake()
        {
            userUIData = Resources.LoadAll<UserScriptableObject>(MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH);
        }

        public void UpdateUserUI(UserScriptableObject userData)
        {
            OnUserChanged?.Invoke(userData);
        }

        public void UpdateMinigameUI(UserScriptableObject userData)
        {
            OnUserChanged?.Invoke(userData);
            OnHealthBarChanged?.Invoke(1);
        }

        public void UpdateTittleUI(TittleScriptableObject tittle)
        {
            OnTittleChanged?.Invoke(tittle);
        }

        public void UpdateScoreUI(bool animation)
        {
            OnLivesChanged?.Invoke(GameManager.instance.GetLives, animation);
        }

        public void UpdateHealthBarUI(float percent)
        {
            OnHealthBarChanged?.Invoke(percent);
        }

        public TittleScriptableObject GetTittle(int minigameID)
        {
            return Resources.LoadAll<TittleScriptableObject>(MINIGAME_TITTLE_UI_SCRIPTABLEOBJECT_PATH)[minigameID];
        }

        public UserScriptableObject GetUser(int minigameID)
        {
            userUIData = Resources.LoadAll<UserScriptableObject>(MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH + minigameID);
            return userUIData.GetRandom();
        }
    }

}