using UnityEngine;

[CreateAssetMenu(
    fileName = "MinigameUI",
    menuName = "ScriptableObjects/Minigames/UI/MinigameScriptableObject"
)]
public class MinigameScriptableObject : ScriptableObject
{
    [Header("Bar Configuration")]
    public Sprite brainSprite;
    public Sprite barSprite;
    public AudioClip barAudioClip;

    [Header("Shake Configuration")]
    public float speed = 0;
    public float interval = 0;
    public float duration = 5;
}
