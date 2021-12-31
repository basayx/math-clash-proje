using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUICardCode : UICardCode
{
    public DefenderGameplayManager gameplayManager;

    public string classInfo;
    public int areaCount = 1;
    [TextArea]
    public string description;

    public GameObject viewObjectPrefab;


    public void GamePointerOnDownFunction0()
    {
        if (gameplayManager.selectedTrapCard == null)
        {
            gameplayManager.ATrapCardSelected(this);
        }
    }
}
