using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameBag
{
    public static HashSet<int> minigameIDs = new HashSet<int>();
    public static int numberOfMinigames = 10; // Set this to the total number of minigames available

    static void Initializebag() {

        numberOfMinigames = Resources.LoadAll<GameObject>(Data.Minigame.MINIGAME_OBJECTS_PREFABS_PATH).Count();

        for (int i = 0; i < numberOfMinigames; i++)
        {
            minigameIDs.Add(i);
        }
    }

    public static int GetRandomMinigame()
    {
        if(minigameIDs.Count == 0)
        {
            Initializebag();
        }

        int randomIndex = minigameIDs.GetRandom();
        minigameIDs.Remove(randomIndex);
        return randomIndex;
    }
}
