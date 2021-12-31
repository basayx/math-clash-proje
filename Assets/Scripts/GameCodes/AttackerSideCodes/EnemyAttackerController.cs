using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackerController : MonoBehaviour
{
    public EnemyAttackerManager attackerManager;

    public int deckCardsCount = 3;

    public List<AttackerCharacterCode> spawnedActiveCharacters = new List<AttackerCharacterCode>();

    // Start is called before the first frame update
    void Start()
    {
        deckCardsCount = Random.Range(deckCardsCount, deckCardsCount + 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitingToDefenderCase()
    {

    }

    public void DecisionCase()
    {
        StartCoroutine(Thinking());
        IEnumerator Thinking()
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            if(Random.Range(0, 4) == 0 && spawnedActiveCharacters.Count < 2)
            {
                AttackerUICardCode selectedCard = attackerManager.matchManager.attackerCardPrefabs[Random.Range(0, attackerManager.matchManager.attackerCardPrefabs.Length)];

                List<RoadLine> availableRoads = new List<RoadLine>();
                foreach(RoadLine road in attackerManager.roadLines)
                {
                    if(selectedCard.classInfo.Contains(road.defenderCannon.classInfo))
                    {
                        availableRoads.Add(road);
                    }
                }

                RoadLine selectedRoad = availableRoads[Random.Range(0, availableRoads.Count)];

                if(deckCardsCount > 0)
                    attackerManager.AnAttackerCharacterSpawned(selectedCard, selectedRoad);

                yield return new WaitForSeconds(Random.Range(0f, 2f));

                DecisionCase();
            }
            else
            {
                DecisionCase();
            }
        }
    }

    public void ASpawnedCharacterDied(AttackerCharacterCode attackerCharacter)
    {
        spawnedActiveCharacters.Remove(attackerCharacter);

        if(spawnedActiveCharacters.Count <= 0 && deckCardsCount <= 0)
        {
            attackerManager.matchManager.RoundEnd();
        }
    }
}
