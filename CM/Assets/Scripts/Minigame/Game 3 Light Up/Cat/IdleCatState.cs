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
        cat.CatReset();
        cat.UpdateTombSpriteLayer?.Invoke(cat.transform.localPosition.y);
    }

    public void OnExecute()
    {

    }

    public void OnExit()
    {
    }
}
