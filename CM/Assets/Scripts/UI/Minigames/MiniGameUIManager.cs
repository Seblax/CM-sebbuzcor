using System;
using UnityEngine;
using RandomNumber = UnityEngine.Random;

public class MiniGameUiManager : Singleton<MiniGameUiManager>
{
    public readonly string MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH = "ScriptableObjects/Minigames/UI/Post/0_MinigamePlaceholder";
    UserScriptableObject[] userUIData;
    public Action<UserScriptableObject> updateUI;

    private void Awake()
    {
        userUIData = Resources.LoadAll<UserScriptableObject>(MINIGAME_USER_UI_SCRIPTABLEOBJECT_PATH);

       
    }

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int randomIndex = RandomNumber.Range(0, userUIData.Length);
        UserScriptableObject randomUserData = userUIData[randomIndex];
        updateUI?.Invoke(randomUserData);
    }
}
