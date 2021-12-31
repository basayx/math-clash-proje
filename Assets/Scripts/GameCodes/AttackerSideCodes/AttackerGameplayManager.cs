using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackerGameplayManager : MonoBehaviour
{
    public MatchManager matchManager;

    public Transform cardsDeckParent;
    public List<AttackerUICardCode> playersDeck = new List<AttackerUICardCode>();
    public Transform cardsParent;
    public AttackerUICardCode nextCard = null;
    public Image nextCardCharacterImage;

    public RoadLine[] roadLines;

    public AttackerUICardCode selectedAttackerCard = null;
    public Transform cardDragObject;

    public int maxGemCount = 9;
    private int currentGemCount = 0;
    public float gemFillTime = 10f;
    private float gemFillTimeLeft = 0f;

    [Header("UI")]
    public GameObject waitinToDefenderPanel;
    public GameObject gamePanel;

    public Image gemBarImage;
    public TextMeshProUGUI gemCountText;

    public List<AttackerCharacterCode> spawnedActiveCharacters = new List<AttackerCharacterCode>();

    void Start()
    {
        GetPlayersDeck();
    }

    public void GetPlayersDeck()
    {
        string[] cardInfos = DataManager.Instance.playersAttackerGameDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                foreach (AttackerUICardCode cardPrefab in matchManager.attackerCardPrefabs)
                {
                    AttackerUICardCode card = null;
                    if (cardPrefab.accessName == cardName)
                    {
                        card = Instantiate(cardPrefab, cardsDeckParent);
                        card.ID = cardID;
                        card.UpdateCardVariables();

                        card.pointerOnDownEvent.AddListener(card.GamePointerOnDownFunction0);

                        playersDeck.Add(card);
                        break;
                    }
                }
            }
        }
    }

    public void PreparationCase()
    {
        waitinToDefenderPanel.SetActive(true);
    }

    public void RoundStarted()
    {
        waitinToDefenderPanel.SetActive(false);
        gamePanel.SetActive(true);

        AddAGem();
        DrawANewAttackerCard();
        DrawANewAttackerCard();
        DrawANewAttackerCard();
    }

    void Update()
    {
        if(currentGemCount < maxGemCount)
        {
            if (gemFillTimeLeft < gemFillTime)
            {
                gemFillTimeLeft += 1f * Time.deltaTime;
                gemBarImage.fillAmount = gemFillTimeLeft / gemFillTime;
            }
            else
            {
                AddAGem();
            }
        }

        if(selectedAttackerCard != null)
        {
            cardDragObject.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject.GetComponent<RoadLine>() && selectedAttackerCard.classInfo.Contains(hit.collider.gameObject.GetComponent<RoadLine>().defenderCannon.classInfo))
                    {
                        SpawnAnAttackerCharacter(hit.collider.gameObject.GetComponent<RoadLine>());
                    }
                    else
                    {
                        UnSelectedTheAttackerCard();
                    }
                }
                else
                {
                    UnSelectedTheAttackerCard();
                }

                if (cardDragObject != null)
                    Destroy(cardDragObject.gameObject);
            }
        }
    }

    public void AnAttackerCardSelected(AttackerUICardCode card)
    {
        if(card.gemCount <= currentGemCount)
        {
            selectedAttackerCard = card;
            cardDragObject = Instantiate(card.gameObject, card.transform.parent).transform;

            foreach (RoadLine roadLine in roadLines)
            {
                if (selectedAttackerCard.classInfo.Contains(roadLine.defenderCannon.classInfo))
                {
                    roadLine.gameObject.SetActive(true);
                }
            }
        }
    }

    public void UnSelectedTheAttackerCard()
    {
        selectedAttackerCard = null;

        foreach (RoadLine roadLine in roadLines)
        {
            roadLine.gameObject.SetActive(false);
        }
    }

    public void SpawnAnAttackerCharacter(RoadLine selectedRoad)
    {
        int roadNo = 0;
        string characterID = selectedAttackerCard.ID;
        for (int i = 0; i < roadLines.Length; i++)
        {
            if(selectedRoad == roadLines[i])
            {
                roadNo = i;
                break;
            }
        }


        currentGemCount -= selectedAttackerCard.gemCount;
        gemCountText.text = currentGemCount.ToString() + "/" + maxGemCount.ToString();

        AttackerCharacterCode attackerCharacter = null;
        foreach(AttackerCharacterCode attacker in matchManager.attackerCharacterPrefabs)
        {
            if(attacker.accessName == selectedAttackerCard.accessName)
            {
                attackerCharacter = attacker;
                break;
            }
        }

        AttackerCharacterCode spawnedCharacter = null;
        if (DataManager.Instance.enemyIsNonPlayer == 0)
            spawnedCharacter = Photon.Pun.PhotonNetwork.Instantiate(attackerCharacter.accessName, selectedRoad.spawnPos.position, selectedRoad.spawnPos.rotation, 0 , null).GetComponent<AttackerCharacterCode>();
        else if (DataManager.Instance.enemyIsNonPlayer == 1)
        {
            spawnedCharacter = Instantiate(attackerCharacter, selectedRoad.spawnPos.position, selectedRoad.spawnPos.rotation);
            spawnedCharacter.targetRoad = selectedRoad;
            spawnedCharacter.starLevelValue = selectedAttackerCard.starLevelValue;
            spawnedCharacter.matchManager = matchManager;
            matchManager.AttackerSpawnedANewCharacter(spawnedCharacter, selectedRoad);
            spawnedCharacter.SetReady();
        }

        spawnedCharacter.gameObject.tag = "PlayersCharacter";

        spawnedActiveCharacters.Add(spawnedCharacter);

        if (DataManager.Instance.enemyIsNonPlayer == 0)
            spawnedCharacter.gameObject.GetComponent<Photon.Pun.PhotonView>().RPC("SetVariables", Photon.Pun.RpcTarget.AllBuffered, spawnedCharacter.classInfo, roadNo, selectedAttackerCard.starLevelValue);

        Destroy(selectedAttackerCard.gameObject);
        UnSelectedTheAttackerCard();
        DrawANewAttackerCard();
    }

    public void DrawANewAttackerCard()
    {
        if(nextCard == null)
        {
            if (playersDeck.Count > 0)
            {
                nextCard = playersDeck[Random.Range(0, playersDeck.Count)];
                nextCardCharacterImage.sprite = nextCard.cardViewImage.sprite;
            }
            else
            {
                nextCard = null;
                nextCardCharacterImage.sprite = null;
            }
        }

        if (nextCard != null)
        {
            AttackerUICardCode card = Instantiate(nextCard, cardsParent);
            card.gameplayManager = this;
            card.UpdateCardVariables();
            card.pointerOnDownEvent.AddListener(card.GamePointerOnDownFunction0);

            playersDeck.Remove(nextCard);

            if (playersDeck.Count > 0)
            {
                nextCard = playersDeck[Random.Range(0, playersDeck.Count)];
                nextCardCharacterImage.sprite = nextCard.cardViewImage.sprite;
            }
            else
            {
                nextCard = null;
                nextCardCharacterImage.sprite = null;
            }
        }
    }

    public void AddAGem()
    {
        currentGemCount++;
        gemCountText.text = currentGemCount.ToString() + "/" + maxGemCount.ToString();
        gemFillTimeLeft = 0f;
        gemBarImage.fillAmount = gemFillTime / gemFillTime;
    }

    public void ASpawnedCharacterDied(AttackerCharacterCode attackerCharacter)
    {
        spawnedActiveCharacters.Remove(attackerCharacter);
        if (spawnedActiveCharacters.Count <= 0 && playersDeck.Count <= 0 && cardsParent.transform.childCount <= 1)
            matchManager.RoundEnd();
    }
}
