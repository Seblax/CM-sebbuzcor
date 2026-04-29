using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System;
using UnityEngine;
using RandomNumber = UnityEngine.Random;

public class MinigameUIManager : Singleton<MinigameUIManager>
{
    public readonly string MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH = "ScriptableObjects/Minigames/UI/User/";
    public readonly string MINIGAME_TITTLE_UI_SCRIPTABLEOBJECT_PATH = "ScriptableObjects/Minigames/UI/Tittle/";
    
    UserScriptableObject[] userUIData;

    public Action<float> OnHealthBarChanged;
    public Action<TittleScriptableObject> OnTittleChanged;
    public Action<UserScriptableObject> OnUserChanged;
    public Action<UserScriptableObject> OnScoreChanged;

    public GameObject tittle;
    public GameObject minigame;
    public GameObject score;

    private void Awake()
    {
        userUIData = Resources.LoadAll<UserScriptableObject>(MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH);
    }

    public void UpdateUserUI(UserScriptableObject userData)
    {
        OnUserChanged?.Invoke(userData);
    }

    public void UpdateTittleUI(TittleScriptableObject tittle)
    {
        OnTittleChanged?.Invoke(tittle);
    }

    public void UpdateScoreUI()
    {
        int randomIndex = RandomNumber.Range(0, userUIData.Length);
        UserScriptableObject randomUserData = userUIData[randomIndex];
        OnUserChanged?.Invoke(randomUserData);
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
