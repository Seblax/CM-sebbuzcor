using Minigame.Game3;
using StateManagement;
using UnityEngine;

public class IdleCatState : IState
{
    private Cat cat;
    public IdleCatState(Cat cat)
    {
        this.cat = cat;

    }
    public void OnEnter()
    {
        AudioManager.instance.PlayEffect("GraveyardMusic");
        cat.CatReset();
    }

    public void OnExecute()
    {

    }

    public void OnExit()
    {
    }
}
