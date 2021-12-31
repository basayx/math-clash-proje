using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackerManager : MonoBehaviour
{
    public MatchManager matchManager;

    public EnemyAttackerController enemyAttackerController;

    public RoadLine[] roadLines;

    public Transform cardsDeckParent;
    public List<AttackerUICardCode> enemysDeck = new List<AttackerUICardCode>();

    void Start()
    {

    }

    public void GetEnemysDeck()
    {
        string[] cardInfos = DataManager.Instance.enemysAttackerGameDeckInfo.Split('#');

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

                        enemysDeck.Add(card);
                    }
                }
            }
        }
    }

    public void PreparationCase()
    {
        if (enemyAttackerController.gameObject.activeSelf)
        {
            enemyAttackerController.WaitingToDefenderCase();
        }
        else
        {
            //TODO MULTIPLAYER
        }
    }

    public void RoundStarted()
    {
        if (enemyAttackerController.gameObject.activeSelf)
        {
            enemyAttackerController.DecisionCase();
        }
        else
        {
            GetEnemysDeck();
            //TODO MULTIPLAYER
        }
    }

    public void AnAttackerCharacterSpawned(string attackerCharacterID, int selectedRoadNo)
    {
        foreach(AttackerUICardCode card in enemysDeck)
        {
            if(card.ID == attackerCharacterID)
            {
                AnAttackerCharacterSpawned(card, roadLines[selectedRoadNo]);
                break;
            }
        }
    }

    public void AnAttackerCharacterSpawned(AttackerUICardCode selectedAttackerCard, RoadLine selectedRoad)
    {
        AttackerCharacterCode attackerCharacter = null;
        foreach (AttackerCharacterCode attacker in matchManager.attackerCharacterPrefabs)
        {
            if (attacker.accessName == selectedAttackerCard.accessName)
            {
                attackerCharacter = attacker;
                break;
            }
        }

        AttackerCharacterCode spawnedCharacter = Instantiate(attackerCharacter, null);
        spawnedCharacter.gameObject.tag = "EnemysCharacter";
        spawnedCharacter.matchManager = matchManager;
        spawnedCharacter.classInfo = selectedAttackerCard.classInfo;
        spawnedCharacter.transform.position = selectedRoad.spawnPos.position;
        spawnedCharacter.transform.rotation = selectedRoad.spawnPos.rotation;
        spawnedCharacter.targetRoad = selectedRoad;

        //Star Level
        spawnedCharacter.starLevelValue = selectedAttackerCard.starLevelValue;

        if (enemyAttackerController.gameObject.activeSelf)
        {
            enemyAttackerController.spawnedActiveCharacters.Add(spawnedCharacter);
            enemyAttackerController.deckCardsCount--;
        }
        matchManager.AttackerSpawnedANewCharacter(spawnedCharacter, selectedRoad);

        spawnedCharacter.SetReady();
    }
}
