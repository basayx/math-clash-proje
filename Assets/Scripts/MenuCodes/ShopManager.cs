using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    public DeckManager DeckManager;

    public GameObject ShopResultPanel;
    public Transform ShopResultCardsParent;
    public Transform ShopResultReferanceCard;
    public GameObject TamamdirButton;

    public int newAttackerCardPrice = 200;

    public int newDefenderCardPrice = 200;

    public int newMixCardPackPrice = 300;

    public void BuyANewAttackerCard()
    {
        if(DataManager.Instance.Coin >= newAttackerCardPrice)
        {
            AddARandomAttackerCardToDeck();
            ShopResultPanel.SetActive(true);
            TamamdirButton.SetActive(true);
            DataManager.Instance.Coin -= newAttackerCardPrice;
        }
    }

    public void AddARandomAttackerCardToDeck()
    {
        AttackerUICardCode targetCard = DeckManager.attackerCardPrefabs[Random.Range(0, DeckManager.attackerCardPrefabs.Length)];

        DataManager.Instance.playersAttackerDeckInfo += targetCard.accessName + "_" + (DataManager.Instance.playersAttackerDeckInfo.Split('#').Length) + "|0" + "#";

        StartCoroutine(CardShowCoroutine(targetCard));
    }

    IEnumerator CardShowCoroutine(UICardCode targetCard)
    {
        Transform cardView = Instantiate(targetCard, ShopResultCardsParent).transform;
        cardView.gameObject.GetComponent<RectTransform>().sizeDelta = ShopResultReferanceCard.gameObject.GetComponent<RectTransform>().sizeDelta;
        cardView.gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
        cardView.transform.localPosition = ShopResultReferanceCard.transform.localPosition;
        cardView.transform.DOScale(ShopResultReferanceCard.localScale * 1.35f, 0.5f);
        cardView.transform.DOLocalMove(ShopResultReferanceCard.transform.localPosition, 0.4f);
        yield return new WaitForSeconds(1f);
        cardView.transform.DOScale(ShopResultReferanceCard.localScale, 0.5f);
        cardView.transform.DOLocalMove(ShopResultReferanceCard.transform.localPosition, 0.4f);
    }

    public void BuyANewDefenderCard()
    {
        if (DataManager.Instance.Coin >= newDefenderCardPrice)
        {
            AddARandomDefenderCardToDeck();
            ShopResultPanel.SetActive(true);
            TamamdirButton.SetActive(true);
            DataManager.Instance.Coin -= newDefenderCardPrice;
        }
    }

    public void AddARandomDefenderCardToDeck()
    {
        if (Random.Range(0, 2) == 0)
        {
            //cannon

            DefenderCannonUICardCode targetCard = DeckManager.defenderCannonCardPrefabs[Random.Range(0, DeckManager.defenderCannonCardPrefabs.Length)];

            DataManager.Instance.playersDefenderCannonDeckInfo += targetCard.accessName + "_" + (DataManager.Instance.playersDefenderCannonDeckInfo.Split('#').Length) + "|0" + "#";

            StartCoroutine(CardShowCoroutine(targetCard));
        }
        else
        {
            TrapUICardCode targetCard = DeckManager.defenderTrapCardPrefabs[Random.Range(0, DeckManager.defenderTrapCardPrefabs.Length)];

            DataManager.Instance.playersTrapDeckInfo += targetCard.accessName + "_" + (DataManager.Instance.playersTrapDeckInfo.Split('#').Length) + "|0" + "#";

            StartCoroutine(CardShowCoroutine(targetCard));
        }
    }

    public void BuyNewTripleMixCardPack()
    {
        if (DataManager.Instance.Coin >= newMixCardPackPrice)
        {
            ShopResultPanel.SetActive(true);
            DataManager.Instance.Coin -= newMixCardPackPrice;

            StartCoroutine(MixPackCoroutine());
            IEnumerator MixPackCoroutine()
            {
                for(int i = 0; i < 3; i++)
                {
                    if (Random.Range(0, 2) == 0)
                        AddARandomAttackerCardToDeck();
                    else
                        AddARandomDefenderCardToDeck();
                    yield return new WaitForSeconds(1.5f);
                }

                ShopResultCardsParent.gameObject.GetComponent<HorizontalLayoutGroup>().enabled = false;
                yield return new WaitForSeconds(0.01f);
                ShopResultCardsParent.gameObject.GetComponent<HorizontalLayoutGroup>().enabled = true;

                TamamdirButton.SetActive(true);
            }
        }
    }

    public void CloseShopResultPanel()
    {
        foreach(Transform child in ShopResultCardsParent)
        {
            if(child != ShopResultReferanceCard)
                Destroy(child.gameObject);
        }
        TamamdirButton.SetActive(false);
        ShopResultPanel.SetActive(false);
    }
}
