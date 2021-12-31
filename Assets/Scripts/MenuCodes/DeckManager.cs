using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public MenuManager menuManager;

    public AttackerUICardCode[] attackerCardPrefabs;
    public AttackerCharacterCode[] attackerPrefabs;
    public Transform attackerDeckCardsParent;
    public GameObject attackerCardAddButtonObject;
    public TextMeshProUGUI attackerCardsCountText;

    public DefenderCannonUICardCode[] defenderCannonCardPrefabs;
    public DefenderCannonCode[] defenderCannonPrefabs;
    public Transform[] defenderCannonParents;

    public TrapUICardCode[] defenderTrapCardPrefabs;
    public TrapObject[] defenderTrapPrefabs;
    public Transform defenderTrapParent;

    public Scrollbar scrollbar;

    [Header("Card View")]
    public GameObject cardViewPanel;
    public UICardCode inViewTargetCard;
    public Image cardNameBorderImage;
    public TextMeshProUGUI cardNameText;
    public Image cardCharacterImage;
    public Image cardImageBorderImage;
    public float cardCurrentExpValue;
    public Image cardExpBarImage;
    public GameObject canGainExpInfoPanel;
    public GameObject maxLevelInfoPanel;
    public GameObject disposableCardsPanel;
    public Transform cardDisposableCardsParent;

    public GameObject[] attackerCardViewObjects;
    public GameObject[] cardTypeInfoGroups;
    public GameObject[] cardClassIcons;
    public GameObject[] cardStarLevelIcons;
    public TextMeshProUGUI cardGemCountText;
    public TextMeshProUGUI cardAttackPowerText;
    public TextMeshProUGUI cardHeathAmountText;
    public TextMeshProUGUI cardSpeedInfoText;

    public GameObject[] defenderCardViewObjects;
    public TextMeshProUGUI defenderCannonPowerText;
    public TextMeshProUGUI defenderCannonRangeText;
    public TextMeshProUGUI defenderCannonMaxAmmoText;
    public TextMeshProUGUI defenderCannonSpeedText;

    public GameObject[] trapCardViewObjects;
    public TextMeshProUGUI trapCardDescription;
    public TextMeshProUGUI trapCardAreaInfoText;


    [Header("Card Change")]
    public UICardCode inChangeTargetCard;
    public bool isInCardChange = false;

    public GameObject cardChangePanel;
    public GameObject inChangeCardViewSlotGroup;
    public Transform inChangeCardViewSlot;
    public Transform cardsListParent;

    void Start()
    {
        UpdateCardLists();
    }

    public void SaldiriDuzeniKisminaGec()
    {
        scrollbar.onValueChanged.Invoke(0.9f);
    }

    public void SavunmaDuzeniKisminaGec()
    {
        scrollbar.onValueChanged.Invoke(0.1f);
    }

    public void UpdateCardLists()
    {
        CloseCardChangePanel();

        UpdateAttackerDeckList();
        UpdateDefenderCannonDeckList();
        UpdateDefenderTrapDeckList();
    }

    public void UpdateAttackerDeckList()
    {
        foreach (Transform child in attackerDeckCardsParent)
        {
            if (child.gameObject != attackerCardAddButtonObject)
            {
                Destroy(child.gameObject);
            }
        }

        string[] cardInfos = DataManager.Instance.playersAttackerGameDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                foreach (AttackerUICardCode cardPrefab in attackerCardPrefabs)
                {
                    AttackerUICardCode targetCard = null;
                    if (cardPrefab.accessName == cardName)
                    {
                        targetCard = Instantiate(cardPrefab, attackerDeckCardsParent);
                        targetCard.ID = cardID;
                        targetCard.UpdateCardVariables();

                        targetCard.deckEditUICard = targetCard.gameObject.AddComponent<DeckEditUICard>();
                        targetCard.deckEditUICard.deckManager = this;
                        targetCard.pointerOnDownEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnDownFunction0);
                        targetCard.pointerOnUpEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnUpFunction0);
                    }
                }
            }
        }

        attackerCardsCountText.text = (cardInfos.Length - 1).ToString();
    }

    public void UpdateDefenderCannonDeckList()
    {
        foreach (DefenderCannonUICardCode cardPrefab in defenderCannonCardPrefabs)
        {
            DefenderCannonUICardCode targetCard = null;
            if (DataManager.Instance.playersToplamaDefenderCannon.Split('_')[0] == cardPrefab.accessName)
            {
                targetCard = Instantiate(cardPrefab, defenderCannonParents[0]);
                targetCard.ID = DataManager.Instance.playersToplamaDefenderCannon;
                targetCard.UpdateCardVariables();

                DefenderCannonCode defenderCannon = null;
                foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
                {
                    if (defenderCannonPrefab.accessName == targetCard.accessName)
                    {
                        defenderCannon = defenderCannonPrefab;
                        break;
                    }
                }
                targetCard.damageAmountText.text = (defenderCannon.damageAmount * (targetCard.starLevelValue + 1)).ToString();

                targetCard.deckEditUICard = targetCard.gameObject.AddComponent<DeckEditUICard>();
                targetCard.deckEditUICard.deckManager = this;
                targetCard.pointerOnDownEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnDownFunction0);
                targetCard.pointerOnUpEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnUpFunction0);
            }
            if (DataManager.Instance.playersCikartmaDefenderCannon.Split('_')[0] == cardPrefab.accessName)
            {
                targetCard = Instantiate(cardPrefab, defenderCannonParents[1]);
                targetCard.ID = DataManager.Instance.playersCikartmaDefenderCannon;
                targetCard.UpdateCardVariables();

                DefenderCannonCode defenderCannon = null;
                foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
                {
                    if (defenderCannonPrefab.accessName == targetCard.accessName)
                    {
                        defenderCannon = defenderCannonPrefab;
                        break;
                    }
                }
                targetCard.damageAmountText.text = (defenderCannon.damageAmount * (targetCard.starLevelValue + 1)).ToString();

                targetCard.deckEditUICard = targetCard.gameObject.AddComponent<DeckEditUICard>();
                targetCard.deckEditUICard.deckManager = this;
                targetCard.pointerOnDownEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnDownFunction0);
                targetCard.pointerOnUpEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnUpFunction0);
            }
            if (DataManager.Instance.playersCarpmaDefenderCannon.Split('_')[0] == cardPrefab.accessName)
            {
                targetCard = Instantiate(cardPrefab, defenderCannonParents[2]);
                targetCard.ID = DataManager.Instance.playersCarpmaDefenderCannon;
                targetCard.UpdateCardVariables();

                DefenderCannonCode defenderCannon = null;
                foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
                {
                    if (defenderCannonPrefab.accessName == targetCard.accessName)
                    {
                        defenderCannon = defenderCannonPrefab;
                        break;
                    }
                }
                targetCard.damageAmountText.text = (defenderCannon.damageAmount * (targetCard.starLevelValue + 1)).ToString();

                targetCard.deckEditUICard = targetCard.gameObject.AddComponent<DeckEditUICard>();
                targetCard.deckEditUICard.deckManager = this;
                targetCard.pointerOnDownEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnDownFunction0);
                targetCard.pointerOnUpEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnUpFunction0);
            }
            if (DataManager.Instance.playersBolmeDefenderCannon.Split('_')[0] == cardPrefab.accessName)
            {
                targetCard = Instantiate(cardPrefab, defenderCannonParents[3]);
                targetCard.ID = DataManager.Instance.playersBolmeDefenderCannon;
                targetCard.UpdateCardVariables();

                DefenderCannonCode defenderCannon = null;
                foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
                {
                    if (defenderCannonPrefab.accessName == targetCard.accessName)
                    {
                        defenderCannon = defenderCannonPrefab;
                        break;
                    }
                }
                targetCard.damageAmountText.text = (defenderCannon.damageAmount * (targetCard.starLevelValue + 1)).ToString();

                targetCard.deckEditUICard = targetCard.gameObject.AddComponent<DeckEditUICard>();
                targetCard.deckEditUICard.deckManager = this;
                targetCard.pointerOnDownEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnDownFunction0);
                targetCard.pointerOnUpEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnUpFunction0);
            }
        }
    }

    public void UpdateDefenderTrapDeckList()
    {
        foreach (Transform child in defenderTrapParent)
        {
            Destroy(child.gameObject);
        }

        string[] cardInfos = DataManager.Instance.playersTrapDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                foreach (TrapUICardCode cardPrefab in defenderTrapCardPrefabs)
                {
                    TrapUICardCode targetCard = null;
                    if (cardPrefab.accessName == cardName)
                    {
                        targetCard = Instantiate(cardPrefab, defenderTrapParent);
                        targetCard.ID = cardID;
                        targetCard.UpdateCardVariables();

                        targetCard.deckEditUICard = targetCard.gameObject.AddComponent<DeckEditUICard>();
                        targetCard.deckEditUICard.deckManager = this;
                        targetCard.pointerOnDownEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnDownFunction0);
                        targetCard.pointerOnUpEvent.AddListener(targetCard.deckEditUICard.MenuPointerOnUpFunction0);
                    }
                }
            }
        }
    }

    public void OpenViewPanelToThisCard(AttackerUICardCode targetCard)
    {
        cardNameBorderImage.color = targetCard.cardBorderImage.color;
        cardNameText.text = targetCard.fullName;
        cardCharacterImage.sprite = targetCard.cardViewImage.sprite;
        cardImageBorderImage.color = targetCard.cardBorderImage.color;

        foreach (GameObject obj in defenderCardViewObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in trapCardViewObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in attackerCardViewObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject typeInfoObject in cardTypeInfoGroups)
        {
            typeInfoObject.SetActive(false);
            if (typeInfoObject.name == targetCard.chracterTypeName)
            {
                typeInfoObject.SetActive(true);
            }
        }

        foreach (GameObject classIcon in cardClassIcons)
        {
            if (targetCard.classInfo.Contains(classIcon.name))
            {
                classIcon.SetActive(true);
            }
            else
            {
                classIcon.SetActive(false);
            }
        }

        for(int i = 0; i < targetCard.starLevelValue; i++)
        {
            cardStarLevelIcons[i].SetActive(true);
        }

        canGainExpInfoPanel.SetActive(true);
        maxLevelInfoPanel.SetActive(false);
        if (targetCard.starLevelValue >= 3)
        {
            canGainExpInfoPanel.SetActive(false);
            maxLevelInfoPanel.SetActive(true);
        }

        cardGemCountText.text = targetCard.gemCount.ToString();

        AttackerCharacterCode attackerCharacter = null;
        foreach(AttackerCharacterCode attackerPrefab in attackerPrefabs)
        {
            if(attackerPrefab.accessName == targetCard.accessName)
            {
                attackerCharacter = attackerPrefab;
                break;
            }
        }

        cardAttackPowerText.text = (attackerCharacter.attackDamageAmount * (targetCard.starLevelValue + 1)).ToString();
        cardHeathAmountText.text = (attackerCharacter.maxHealthAmount * (targetCard.starLevelValue + 1)).ToString();
        if((attackerCharacter.defaultMovementSpeed * (targetCard.starLevelValue + 1)) <= ConstantVariables.Yavas_maxAttackerMovementSpeed)
        {
            cardSpeedInfoText.text = "Yavaş";
        }
        else if ((attackerCharacter.defaultMovementSpeed * (targetCard.starLevelValue + 1)) <= ConstantVariables.Normal_maxAttackerMovementSpeed)
        {
            cardSpeedInfoText.text = "Normal";
        }
        else if((attackerCharacter.defaultMovementSpeed * (targetCard.starLevelValue + 1)) > ConstantVariables.Normal_maxAttackerMovementSpeed)
        {
            cardSpeedInfoText.text = "Hızlı";
        }

        inViewTargetCard = targetCard;
        cardCurrentExpValue = PlayerPrefs.GetFloat(targetCard.ID + "_currentExpValue");
        if(targetCard.starLevelValue < 3)
        {
            cardExpBarImage.fillAmount = cardCurrentExpValue / 100f;
            UpdateDisposableCardsListToInViewTargetCard();
        }

        menuManager.KartDetayPanelineGecButton();
    }

    public void OpenViewPanelToThisCard(DefenderCannonUICardCode targetCard)
    {
        cardNameBorderImage.color = targetCard.cardBorderImage.color;
        cardNameText.text = targetCard.fullName;
        cardCharacterImage.sprite = targetCard.cardViewImage.sprite;
        cardImageBorderImage.color = targetCard.cardBorderImage.color;

        for (int i = 0; i < targetCard.starLevelValue; i++)
        {
            cardStarLevelIcons[i].SetActive(true);
        }

        canGainExpInfoPanel.SetActive(true);
        maxLevelInfoPanel.SetActive(false);
        if (targetCard.starLevelValue >= 3)
        {
            canGainExpInfoPanel.SetActive(false);
            maxLevelInfoPanel.SetActive(true);
        }

        foreach(GameObject obj in attackerCardViewObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in trapCardViewObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in defenderCardViewObjects)
        {
            obj.SetActive(true);
        }

        DefenderCannonCode defenderCannon = null;
        foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
        {
            if (defenderCannonPrefab.accessName == targetCard.accessName)
            {
                defenderCannon = defenderCannonPrefab;
                break;
            }
        }

        defenderCannonPowerText.text = (defenderCannon.damageAmount * (targetCard.starLevelValue + 1)).ToString();

        if ((defenderCannon.maxZDistance * (targetCard.starLevelValue + 1)) <= ConstantVariables.YakinMesafeli_maxCannonRangeValue)
        {
            defenderCannonRangeText.text = "Yakın Mesafeli";
        }
        else if ((defenderCannon.maxZDistance * (targetCard.starLevelValue + 1)) <= ConstantVariables.NormalMesafeli_maxCannonRangeValue)
        {
            defenderCannonRangeText.text = "Normal Mesafeli";
        }
        else if ((defenderCannon.maxZDistance * (targetCard.starLevelValue + 1)) > ConstantVariables.NormalMesafeli_maxCannonRangeValue)
        {
            defenderCannonRangeText.text = "Uzak Mesafeli";
        }

        defenderCannonMaxAmmoText.text = (defenderCannon.maxAmmoAmount * (targetCard.starLevelValue + 1)) .ToString();

        if ((defenderCannon.attackCooldown / (targetCard.starLevelValue + 1))  < ConstantVariables.Normal_maxCannonReloadSpeed)
        {
            defenderCannonSpeedText.text = "Hızlı";
        }
        else if ((defenderCannon.attackCooldown / (targetCard.starLevelValue + 1))  < ConstantVariables.Yavas_maxCannonReloadSpeed)
        {
            defenderCannonSpeedText.text = "Normal";
        }
        else if ((defenderCannon.attackCooldown / (targetCard.starLevelValue + 1))  >= ConstantVariables.Yavas_maxCannonReloadSpeed)
        {
            defenderCannonSpeedText.text = "Yavaş";
        }

        inViewTargetCard = targetCard;
        cardCurrentExpValue = PlayerPrefs.GetFloat(targetCard.ID + "_currentExpValue");
        if (targetCard.starLevelValue < 3)
        {
            cardExpBarImage.fillAmount = cardCurrentExpValue / 100f;
            UpdateDisposableCardsListToInViewTargetCard();
        }

        menuManager.KartDetayPanelineGecButton();
    }

    public void OpenViewPanelToThisCard(TrapUICardCode targetCard)
    {
        cardNameBorderImage.color = targetCard.cardBorderImage.color;
        cardNameText.text = targetCard.fullName;
        cardCharacterImage.sprite = targetCard.cardViewImage.sprite;
        cardImageBorderImage.color = targetCard.cardBorderImage.color;

        disposableCardsPanel.SetActive(false);
        canGainExpInfoPanel.SetActive(false);
        maxLevelInfoPanel.SetActive(false);

        foreach (GameObject obj in attackerCardViewObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in defenderCardViewObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in trapCardViewObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject classIcon in cardClassIcons)
        {
            if (targetCard.classInfo.Contains(classIcon.name))
            {
                classIcon.SetActive(true);
            }
            else
            {
                classIcon.SetActive(false);
            }
        }

        trapCardDescription.text = targetCard.description;
        trapCardAreaInfoText.text = targetCard.areaCount.ToString();

        inViewTargetCard = targetCard;

        menuManager.KartDetayPanelineGecButton();
    }

    public void UpdateDisposableCardsListToInViewTargetCard()
    {
        disposableCardsPanel.SetActive(true);

        foreach (Transform child in cardDisposableCardsParent)
        {
            Destroy(child.gameObject);
        }

        string[] cardInfos = new string[0];
        if(inViewTargetCard.GetComponent<AttackerUICardCode>())
            cardInfos = DataManager.Instance.playersAttackerDeckInfo.Split('#');
        else if(inViewTargetCard.GetComponent<DefenderCannonUICardCode>())
            cardInfos = DataManager.Instance.playersDefenderCannonDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                if (cardName == inViewTargetCard.accessName && inViewTargetCard.starLevelValue == cardStarLevel)
                {
                    List<UICardCode> cardPrefabs = new List<UICardCode>();

                    if (inViewTargetCard.GetComponent<AttackerUICardCode>())
                        cardPrefabs = new List<UICardCode>(attackerCardPrefabs);
                    else if (inViewTargetCard.GetComponent<DefenderCannonUICardCode>())
                        cardPrefabs = new List<UICardCode>(defenderCannonCardPrefabs);

                    foreach (UICardCode cardPrefab in cardPrefabs)
                    {
                        UICardCode disposableCard = null;
                        if (cardPrefab.accessName == cardName)
                        {
                            disposableCard = Instantiate(cardPrefab, cardDisposableCardsParent);
                            disposableCard.ID = cardID;
                            disposableCard.UpdateCardVariables();

                            if (disposableCard.GetComponent<DefenderCannonUICardCode>())
                            {
                                DefenderCannonCode defenderCannon = null;
                                foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
                                {
                                    if (defenderCannonPrefab.accessName == disposableCard.accessName)
                                    {
                                        defenderCannon = defenderCannonPrefab;
                                        break;
                                    }
                                }

                                disposableCard.gameObject.GetComponent<DefenderCannonUICardCode>().damageAmountText.text = (defenderCannon.damageAmount * (disposableCard.starLevelValue + 1)).ToString();
                            }

                            disposableCard.deckEditUICard = disposableCard.gameObject.AddComponent<DeckEditUICard>();
                            disposableCard.deckEditUICard.disposableCard = true;
                            disposableCard.deckEditUICard.deckManager = this;
                            disposableCard.pointerOnDownEvent.AddListener(disposableCard.deckEditUICard.MenuPointerOnDownFunction0);
                        }
                    }
                }
            }
        }
    }

    public void DisposeTargetCardToInViewCard(AttackerUICardCode targetCard)
    {
        string[] cardInfos = DataManager.Instance.playersAttackerDeckInfo.Split('#');
        string updatedCardInfos = "";

        foreach(string cardInfo in cardInfos)
        {
            if(cardInfo != targetCard.ID)
            {
                updatedCardInfos += cardInfo + "#";
            }
        }
        DataManager.Instance.playersAttackerDeckInfo = updatedCardInfos;

        cardCurrentExpValue += PlayerPrefs.GetFloat(inViewTargetCard.ID + "_currentExpValue") + ConstantVariables.ExpGainValuePerCard;

        if(cardCurrentExpValue >= 100f)
        {
            cardCurrentExpValue = 0f;
            inViewTargetCard.starLevelValue += 1;
            for (int i = 0; i < inViewTargetCard.starLevelValue; i++)
            {
                cardStarLevelIcons[i].SetActive(true);
            }

            if (inViewTargetCard.starLevelValue >= 3)
            {
                canGainExpInfoPanel.SetActive(false);
                maxLevelInfoPanel.SetActive(true);

                foreach (Transform child in cardDisposableCardsParent)
                {
                    Destroy(child.gameObject);
                }
            }

            UpdateTheCardFromGameDeck(inViewTargetCard.GetComponent<AttackerUICardCode>());
        }

        PlayerPrefs.SetFloat(inViewTargetCard.ID + "_currentExpValue", cardCurrentExpValue);
        cardExpBarImage.DOKill();
        if (cardExpBarImage.fillAmount < (cardCurrentExpValue / 100f))
            cardExpBarImage.DOFillAmount(cardCurrentExpValue / 100f, 0.75f);
        else
            cardExpBarImage.fillAmount = cardCurrentExpValue / 100f;

        Destroy(targetCard.gameObject);
    }

    public void DisposeTargetCardToInViewCard(DefenderCannonUICardCode targetCard)
    {
        string[] cardInfos = DataManager.Instance.playersDefenderCannonDeckInfo.Split('#');
        string updatedCardInfos = "";

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != targetCard.ID)
            {
                updatedCardInfos += cardInfo + "#";
            }
        }
        DataManager.Instance.playersDefenderCannonDeckInfo = updatedCardInfos;

        cardCurrentExpValue += PlayerPrefs.GetFloat(inViewTargetCard.ID + "_currentExpValue") + ConstantVariables.ExpGainValuePerCard;

        if (cardCurrentExpValue >= 100f)
        {
            cardCurrentExpValue = 0f;
            inViewTargetCard.starLevelValue += 1;
            for (int i = 0; i < inViewTargetCard.starLevelValue; i++)
            {
                cardStarLevelIcons[i].SetActive(true);
            }

            if (inViewTargetCard.starLevelValue >= 3)
            {
                canGainExpInfoPanel.SetActive(false);
                maxLevelInfoPanel.SetActive(true);

                foreach (Transform child in cardDisposableCardsParent)
                {
                    Destroy(child.gameObject);
                }
            }

            UpdateTheCardFromGameDeck(inViewTargetCard.GetComponent<DefenderCannonUICardCode>());
        }

        PlayerPrefs.SetFloat(inViewTargetCard.ID + "_currentExpValue", cardCurrentExpValue);
        cardExpBarImage.DOKill();
        if (cardExpBarImage.fillAmount < (cardCurrentExpValue / 100f))
            cardExpBarImage.DOFillAmount(cardCurrentExpValue / 100f, 0.75f);
        else
            cardExpBarImage.fillAmount = cardCurrentExpValue / 100f;

        Destroy(targetCard.gameObject);
    }

    public void UpdateTheCardFromGameDeck(AttackerUICardCode targetCard)
    {
        string[] cardInfos = DataManager.Instance.playersAttackerGameDeckInfo.Split('#');
        string updatedCardInfos = "";

        for(int i = 0; i < cardInfos.Length; i++)
        {
            if(cardInfos[i] != "")
            {
                if (cardInfos[i] == targetCard.ID)
                {
                    string updatedID = cardInfos[i].Split('|')[0] + "|" + targetCard.starLevelValue.ToString();
                    targetCard.ID = updatedID;
                    cardInfos[i] = updatedID;
                }
                updatedCardInfos += cardInfos[i] + "#";
            }
        }
        DataManager.Instance.playersAttackerGameDeckInfo = updatedCardInfos;
    }

    public void UpdateTheCardFromGameDeck(DefenderCannonUICardCode targetCard)
    {
        string updatedID = targetCard.ID.Split('|')[0] + "|" + targetCard.starLevelValue.ToString();
        targetCard.ID = updatedID;

        if (targetCard.ID == DataManager.Instance.playersToplamaDefenderCannon)
            DataManager.Instance.playersToplamaDefenderCannon = targetCard.ID;
        else if (targetCard.ID == DataManager.Instance.playersCikartmaDefenderCannon)
            DataManager.Instance.playersCikartmaDefenderCannon = targetCard.ID;
        else if (targetCard.ID == DataManager.Instance.playersCarpmaDefenderCannon)
            DataManager.Instance.playersCarpmaDefenderCannon = targetCard.ID;
        else if (targetCard.ID == DataManager.Instance.playersBolmeDefenderCannon)
            DataManager.Instance.playersBolmeDefenderCannon = targetCard.ID;
    }

    public void AddNewCardButton()
    {
        if(DataManager.Instance.playersAttackerGameDeckInfo.Split('#').Length - 1 < ConstantVariables.MaxCardCountForAttackerGameDeck)
            OpenChangePanelToThisCard(null as AttackerUICardCode);
    }

    public void OpenChangePanelToThisCard(AttackerUICardCode targetCard)
    {
        isInCardChange = true;
        foreach (Transform child in cardsListParent)
        {
            if(child.gameObject.name != "removeCardButton")
                Destroy(child.gameObject);
            else
                child.gameObject.SetActive(true);
        }

        string[] cardInfos = DataManager.Instance.playersAttackerDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                foreach (AttackerUICardCode cardPrefab in attackerCardPrefabs)
                {
                    AttackerUICardCode card = null;
                    if (cardPrefab.accessName == cardName)
                    {
                        card = Instantiate(cardPrefab, cardsListParent);
                        card.ID = cardID;
                        card.UpdateCardVariables();

                        card.deckEditUICard = card.gameObject.AddComponent<DeckEditUICard>();
                        card.deckEditUICard.deckManager = this;
                        card.pointerOnDownEvent.AddListener(card.deckEditUICard.CardChangingListPointerOnDownFunction0);
                        card.pointerOnUpEvent.AddListener(card.deckEditUICard.CardChangingListPointerOnUpFunction0);
                    }
                }
            }
        }

        if (targetCard != null)
        {
            foreach(Transform child in inChangeCardViewSlot)
            {
                if (child.gameObject.GetComponent<UICardCode>())
                    Destroy(child.gameObject);
            }

            Instantiate(targetCard, inChangeCardViewSlot).buttonObject.SetActive(false);
            inChangeCardViewSlotGroup.SetActive(true);
        }
        else
        {
            inChangeCardViewSlotGroup.SetActive(false);
        }

        inChangeTargetCard = targetCard;

        menuManager.KartDegisimPanelineGecButton();
    }

    public void OpenChangePanelToThisCard(DefenderCannonUICardCode targetCard)
    {
        isInCardChange = true;
        foreach (Transform child in cardsListParent)
        {
            if (child.gameObject.name != "removeCardButton")
                Destroy(child.gameObject);
            else
                child.gameObject.SetActive(false);
        }

        string[] cardInfos = DataManager.Instance.playersDefenderCannonDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                foreach (DefenderCannonUICardCode cardPrefab in defenderCannonCardPrefabs)
                {
                    DefenderCannonUICardCode card = null;
                    if (cardPrefab.accessName == cardName)
                    {
                        card = Instantiate(cardPrefab, cardsListParent);
                        card.ID = cardID;
                        card.UpdateCardVariables();

                        if (card.GetComponent<DefenderCannonUICardCode>())
                        {
                            DefenderCannonCode defenderCannon = null;
                            foreach (DefenderCannonCode defenderCannonPrefab in defenderCannonPrefabs)
                            {
                                if (defenderCannonPrefab.accessName == card.accessName)
                                {
                                    defenderCannon = defenderCannonPrefab;
                                    break;
                                }
                            }

                            card.gameObject.GetComponent<DefenderCannonUICardCode>().damageAmountText.text = (defenderCannon.damageAmount * (card.starLevelValue + 1)).ToString();
                        }

                        card.deckEditUICard = card.gameObject.AddComponent<DeckEditUICard>();
                        card.deckEditUICard.deckManager = this;
                        card.pointerOnDownEvent.AddListener(card.deckEditUICard.CardChangingListPointerOnDownFunction0);
                        card.pointerOnUpEvent.AddListener(card.deckEditUICard.CardChangingListPointerOnUpFunction0);
                    }
                }
            }
        }

        if (targetCard != null)
        {
            foreach (Transform child in inChangeCardViewSlot)
            {
                if (child.gameObject.GetComponent<UICardCode>())
                    Destroy(child.gameObject);
            }

            Instantiate(targetCard, inChangeCardViewSlot).buttonObject.SetActive(false);
            inChangeCardViewSlotGroup.SetActive(true);
        }
        else
        {
            inChangeCardViewSlotGroup.SetActive(false);
        }

        inChangeTargetCard = targetCard;

        menuManager.KartDegisimPanelineGecButton();
    }

    public void ChangeTheInChangeAttackerCardWithTargetCardOrAddANewCard(AttackerUICardCode targetCard)
    {
        if (inChangeTargetCard != null)
            ChangeTheCardFromGameDeck(inChangeTargetCard.GetComponent<AttackerUICardCode>(), targetCard);
        else
            AddANewCardToGameDeck(targetCard);

        inChangeTargetCard = null;

        CloseCardChangePanel();
        menuManager.DesteDuzenlemePanelineGecButton();
    }

    public void ChangeTheInChangeAttackerCardWithNULL()
    {
        if(inChangeTargetCard != null)
            RemoveTheCardFromGameDeck(inChangeTargetCard.GetComponent<AttackerUICardCode>());

        CloseCardChangePanel();
        menuManager.DesteDuzenlemePanelineGecButton();
    }

    public void ChangeTheInChangeDefenderCannonCardWithTargetCard(DefenderCannonUICardCode targetCard)
    {
        ChangeTheCardFromGameDeck(inChangeTargetCard.GetComponent<DefenderCannonUICardCode>(), targetCard);
    }

    public void RemoveTheCardFromGameDeck(AttackerUICardCode targetCard)
    {
        string[] cardInfos = DataManager.Instance.playersAttackerGameDeckInfo.Split('#');
        string updatedCardInfos = "";

        for (int i = 0; i < cardInfos.Length; i++)
        {
            if (cardInfos[i] != "")
            {
                if (cardInfos[i] != targetCard.ID)
                {
                    updatedCardInfos += cardInfos[i] + "#";
                }
            }
        }

        string updatedCardInfosForGeneralDeck = DataManager.Instance.playersAttackerDeckInfo + targetCard.ID + "#";
        
        DataManager.Instance.playersAttackerGameDeckInfo = updatedCardInfos;
        DataManager.Instance.playersAttackerDeckInfo = updatedCardInfosForGeneralDeck;
    }

    private void ChangeTheCardFromGameDeck(AttackerUICardCode oldCard, AttackerUICardCode newCard)
    {
        string[] cardInfos = DataManager.Instance.playersAttackerGameDeckInfo.Split('#');
        string updatedCardInfos = "";

        for (int i = 0; i < cardInfos.Length; i++)
        {
            if(cardInfos[i] != "")
            {
                if (cardInfos[i] != oldCard.ID)
                {
                    updatedCardInfos += cardInfos[i] + "#";
                }
                else if(newCard != null)
                {
                    updatedCardInfos += newCard.ID + "#";
                }
            }
        }

        string[] cardInfosForGeneralDeck = DataManager.Instance.playersAttackerDeckInfo.Split('#');
        string updatedCardInfosForGeneralDeck = "";

        for (int i = 0; i < cardInfosForGeneralDeck.Length; i++)
        {
            if (cardInfosForGeneralDeck[i] != "")
            {
                if (newCard != null && cardInfosForGeneralDeck[i] != newCard.ID)
                {
                    updatedCardInfosForGeneralDeck += cardInfosForGeneralDeck[i] + "#";
                }
            }
        }
        updatedCardInfosForGeneralDeck += oldCard.ID + "#";

        DataManager.Instance.playersAttackerGameDeckInfo = updatedCardInfos;
        DataManager.Instance.playersAttackerDeckInfo = updatedCardInfosForGeneralDeck;
    }

    private void AddANewCardToGameDeck(AttackerUICardCode newCard)
    {
        string updatedCardInfos = DataManager.Instance.playersAttackerGameDeckInfo + newCard.ID + "#";

        string[] cardInfosForGeneralDeck = DataManager.Instance.playersAttackerDeckInfo.Split('#');
        string updatedCardInfosForGeneralDeck = "";

        for (int i = 0; i < cardInfosForGeneralDeck.Length; i++)
        {
            if (cardInfosForGeneralDeck[i] != "")
            {
                if (cardInfosForGeneralDeck[i] != newCard.ID)
                {
                    updatedCardInfosForGeneralDeck += cardInfosForGeneralDeck[i] + "#";
                }
            }
        }

        DataManager.Instance.playersAttackerGameDeckInfo = updatedCardInfos;
        DataManager.Instance.playersAttackerDeckInfo = updatedCardInfosForGeneralDeck;
    }

    private void ChangeTheCardFromGameDeck(DefenderCannonUICardCode oldCard, DefenderCannonUICardCode newCard)
    {
        if (oldCard.ID == DataManager.Instance.playersToplamaDefenderCannon)
            DataManager.Instance.playersToplamaDefenderCannon = newCard.ID;
        else if (oldCard.ID == DataManager.Instance.playersCikartmaDefenderCannon)
            DataManager.Instance.playersCikartmaDefenderCannon = newCard.ID;
        else if (oldCard.ID == DataManager.Instance.playersCarpmaDefenderCannon)
            DataManager.Instance.playersCarpmaDefenderCannon = newCard.ID;
        else if (oldCard.ID == DataManager.Instance.playersBolmeDefenderCannon)
            DataManager.Instance.playersBolmeDefenderCannon = newCard.ID;

        string[] cardInfosForGeneralDeck = DataManager.Instance.playersDefenderCannonDeckInfo.Split('#');
        string updatedCardInfosForGeneralDeck = "";

        for (int i = 0; i < cardInfosForGeneralDeck.Length; i++)
        {
            if (cardInfosForGeneralDeck[i] != "")
            {
                if (cardInfosForGeneralDeck[i] != newCard.ID)
                {
                    updatedCardInfosForGeneralDeck += cardInfosForGeneralDeck[i] + "#";
                }
            }
        }
        updatedCardInfosForGeneralDeck += oldCard.ID + "#";

        DataManager.Instance.playersDefenderCannonDeckInfo = updatedCardInfosForGeneralDeck;
    }

    public void CloseCardChangePanel()
    {
        for (int i = 0; i < 3; i++)
        {
            cardStarLevelIcons[i].SetActive(false);
        }
        isInCardChange = false;
        inChangeTargetCard = null;
        cardChangePanel.SetActive(false);
    }
}
