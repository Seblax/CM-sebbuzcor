using Minigame;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] GameObject victory;
    [SerializeField] GameObject defeat;

    void Start()
    {
        MinigameUIManager.instance.OnLivesChanged += UpdateScoreUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScoreUI(int i, bool animation) {
        this.victory.SetActive(MinigameManager.instance.minigame.Win);
        this.defeat.SetActive(MinigameManager.instance.minigame.Lose);
    }


}
