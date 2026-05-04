using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "MinigameUI",
    menuName = "ScriptableObjects/Minigames/UI/TittleScriptableObject"
)]

public class TittleScriptableObject : ScriptableObject
{
    private readonly string MINIGAME_TITTLE_UI_SCRIPTABLEOBJECT_ADUSERS_PATH = "ScriptableObjects/Minigames/UI/AddUsers";

    [Header("AD User")]
    public UserScriptableObject[] adUser;

    [Header("Tittle Configuration")]
    [TextArea]
 
    public string tittle;

    public string FormatQuantity(int n)
    {
        // Menor de 1000: devolvemos el string normal
        if (n < 1000)
        {
            return n.ToString();
        }

        // Entre mil y un millón
        if (n < 1000000)
        {
            // Truncamos a 1 decimal: (1555 / 1000 = 1.555) -> (1.555 * 10 = 15.55) -> Truncate = 15 -> 15 / 10 = 1.5
            float kValue = (float)Math.Truncate((n / 1000f) * 10) / 10;
            return kValue.ToString("0.#", System.Globalization.CultureInfo.InvariantCulture) + " k";
        }

        // Mayor a un millón
        float mValue = (float)Math.Truncate((n / 1000000f) * 10) / 10;
        return mValue.ToString("0.#", System.Globalization.CultureInfo.InvariantCulture) + " mill";
    }

    public UserScriptableObject GetAdUser()
    {
        adUser = Resources.LoadAll<UserScriptableObject>(MINIGAME_TITTLE_UI_SCRIPTABLEOBJECT_ADUSERS_PATH);
        return adUser.GetRandom();
    }
}
