using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "MinigameUI",
    menuName = "ScriptableObjects/Minigames/UI/UserScriptableObject"
)]
public class UserScriptableObject : ScriptableObject
{
    [Header("User Configuration")]
    public Sprite userPicture;
    public string userName;
    [TextArea]
    public string userComment;

    [Header("Public Data Configuration")]
    public int likes = 0;
    public int comments = 0;

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
}
