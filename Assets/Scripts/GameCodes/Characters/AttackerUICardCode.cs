using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AttackerUICardCode : UICardCode
{
    public AttackerGameplayManager gameplayManager;
    public GameObject buttonObject;

    public int gemCount = 1;
    public string classInfo = "toplama cikartma carpma bolme";

    public string chracterTypeName = "";

    public void GamePointerOnDownFunction0()
    {
        if (gameplayManager.selectedAttackerCard == null)
        {
            gameplayManager.AnAttackerCardSelected(this);
        }
    }
}
