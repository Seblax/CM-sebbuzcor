using UnityEngine;
using Minigame;

public class PlayerController : MonoBehaviour
{
    private Vector2 startPosition;
    private Hop hop;

    private void Awake()
    {
        InputManager.instance.TapActions += PlayHop;
        InputManager.instance.TouchActions += MovePlayer;

        startPosition = this.transform.localPosition;
        hop = this.GetComponent<Hop>();
    }

    void PlayHop() {
        this.hop.Play();
    }

    void MovePlayer(Vector3 pos)
    {
        Debug.Log($"Position: {pos}");
        transform.position = pos;
    }
}