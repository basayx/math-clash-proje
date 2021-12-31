using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICardCode : MonoBehaviour
{
    public string ID; //cardName_no|starLevel
    public string accessName; //cardName
    public string fullName; //Card Name

    public int starLevelValue = 0;
    public GameObject[] startIcons;

    public Image cardViewImage;
    public Image cardBorderImage;

    public enum KarakterEnderlikleri
    {
        genel,
        nadir,
        super
    }
    public KarakterEnderlikleri karakterEnderligi;

    [Header("Menu Side")]
    public DeckEditUICard deckEditUICard;

    public UnityEvent pointerOnDownEvent = new UnityEvent();
    public UnityEvent pointerOnUpEvent = new UnityEvent();

    public void UpdateCardVariables()
    {
        starLevelValue = int.Parse(ID.Split('|')[1]);
        for (int i = 0; i < starLevelValue; i++)
        {
            startIcons[i].SetActive(true);
        }
    }

    public void PointerOnDown()
    {
        pointerOnDownEvent.Invoke();
    }

    public void PointerOnUp()
    {
        pointerOnUpEvent.Invoke();
    }
}
