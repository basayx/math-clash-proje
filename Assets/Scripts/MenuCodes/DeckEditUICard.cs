using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckEditUICard : MonoBehaviour
{
    public DeckManager deckManager;

    private float minTimeToViewCase = 0.5f;
    [HideInInspector]
    public bool mouseOnIt = false;
    [HideInInspector]
    public float mouseOnTime = 0f;

    public bool disposableCard = false;

    void Update()
    {
        if (mouseOnIt)
        {
            mouseOnTime += 1f * Time.deltaTime;

            if(mouseOnTime > minTimeToViewCase)
            {
                MenuPointerOnUpFunction0();
            }
        }
    }

    public void MenuPointerOnDownFunction0()
    {
        if (disposableCard)
        {
            if(GetComponent<AttackerUICardCode>())
                deckManager.DisposeTargetCardToInViewCard(GetComponent<AttackerUICardCode>());
            else if(GetComponent<DefenderCannonUICardCode>())
                deckManager.DisposeTargetCardToInViewCard(GetComponent<DefenderCannonUICardCode>());
        }
        else
        {
            mouseOnTime = 0f;
            mouseOnIt = true;
        }
    }

    public void MenuPointerOnUpFunction0()
    {
        mouseOnIt = false;
        if (!disposableCard)
        {
            if (mouseOnTime > minTimeToViewCase)
            {
                //view
                if (GetComponent<AttackerUICardCode>())
                    deckManager.OpenViewPanelToThisCard(GetComponent<AttackerUICardCode>());
                else if (GetComponent<DefenderCannonUICardCode>())
                    deckManager.OpenViewPanelToThisCard(GetComponent<DefenderCannonUICardCode>());
                else if (GetComponent<TrapUICardCode>())
                    deckManager.OpenViewPanelToThisCard(GetComponent<TrapUICardCode>());
            }
            else
            {
                //change
                if (GetComponent<AttackerUICardCode>())
                    deckManager.OpenChangePanelToThisCard(GetComponent<AttackerUICardCode>());
                else if (GetComponent<DefenderCannonUICardCode>())
                    deckManager.OpenChangePanelToThisCard(GetComponent<DefenderCannonUICardCode>());
            }
        }
        mouseOnTime = 0f;
    }

    public void CardChangingListPointerOnDownFunction0()
    {
        mouseOnTime = 0f;
        mouseOnIt = true;
    }

    public void CardChangingListPointerOnUpFunction0()
    {
        if (mouseOnIt)
        {
            if (mouseOnTime > minTimeToViewCase)
            {
                //view
                if (GetComponent<AttackerUICardCode>())
                    deckManager.OpenViewPanelToThisCard(GetComponent<AttackerUICardCode>());
                else if(GetComponent<DefenderCannonUICardCode>())
                    deckManager.OpenViewPanelToThisCard(GetComponent<DefenderCannonUICardCode>());
            }
            else
            {
                //change
                if(GetComponent<AttackerUICardCode>())
                    deckManager.ChangeTheInChangeAttackerCardWithTargetCardOrAddANewCard(GetComponent<AttackerUICardCode>());
                else if (GetComponent<DefenderCannonUICardCode>())
                    deckManager.ChangeTheInChangeDefenderCannonCardWithTargetCard(GetComponent<DefenderCannonUICardCode>());
            }
        }
        mouseOnTime = 0f;
        mouseOnIt = false;
    }
}
