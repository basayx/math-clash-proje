using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenderController : MonoBehaviour
{
    public EnemyDefenderManager defenderManager;

    public void SelectTrapsCase()
    {
        if (true)
        {
            int targetTrapCount = Random.Range(1, 6);

            StartCoroutine(Selecting());
            IEnumerator Selecting()
            {
                int currentTrapCount = 0;
                while (currentTrapCount < targetTrapCount)
                {
                    yield return new WaitForSeconds(Random.Range(1f, 2f));

                    TrapUICardCode targetTrap = defenderManager.matchManager.trapCardPrefabs[Random.Range(0, defenderManager.matchManager.trapCardPrefabs.Length)];

                    RoadLine targetRoad = null;
                    foreach (RoadLine road in defenderManager.roadLines)
                    {
                        if (targetTrap.classInfo.Contains(road.defenderCannon.classInfo) && road.potentialTrapPoses.Count > 0)
                        {
                            targetRoad = road;
                            break;
                        }
                    }

                    if(targetRoad != null)
                    {
                        int targetTrapPosNo = Random.Range(0, targetRoad.potentialTrapPoses.Count);

                        defenderManager.ATrapPlacemented(targetTrap.accessName, targetRoad.potentialTrapPoses[targetTrapPosNo].position + new Vector3(0, 0, Random.Range(-1f, 1f)));

                        targetRoad.potentialTrapPoses.RemoveAt(targetTrapPosNo);
                    }

                    currentTrapCount += targetTrap.areaCount;
                }

                defenderManager.matchManager.PreparationCompleted();
            }
        }
    }

    public void SelectACannon()
    {
        DefenderCannonCode targetCannon = defenderManager.cannons[Random.Range(0, defenderManager.cannons.Length)];

        if(targetCannon.attackersOnTheLine.Count <= 0 || Random.Range(0, 4) != 0)
        {
            foreach (DefenderCannonCode cannon in defenderManager.cannons)
            {
                if (cannon.attackersOnTheLine.Count > targetCannon.attackersOnTheLine.Count)
                {
                    targetCannon = cannon;
                }
            }
        }

        if(Random.Range(0, 7) == 0)
            targetCannon.ammoAmount++;
        defenderManager.ACannonSelected(targetCannon);
        GiveRandomAnswer();
    }

    public void GiveRandomAnswer()
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            if(defenderManager.currentSelectedCannon == null)
            {
                yield break;
            }

            while (defenderManager.currentSelectedCannon != null && defenderManager.currentSelectedCannon.currentAttackCooldown > 0f)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(Random.Range(1f, 1.5f));

            if(Random.Range(0,2) == 1)
                defenderManager.QuestionAnswered(true);
            else
                defenderManager.QuestionAnswered(false);

            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            SelectACannon();
        }
    }
}
