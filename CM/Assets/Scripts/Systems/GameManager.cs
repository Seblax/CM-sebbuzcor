using UnityEngine;
using gamemanager;

public class GameManager : MonoBehaviour
{
    public float aceleration = 1f;

    private void LateUpdate()
    {
        Aceleration.SetScale = aceleration;
    }
}
