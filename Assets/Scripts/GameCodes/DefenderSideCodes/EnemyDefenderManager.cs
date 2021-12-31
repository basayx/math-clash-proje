using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyDefenderManager : MonoBehaviour
{
    public MatchManager matchManager;
    public TextMeshProUGUI timerText;

    public DefenderTower defenderTower;

    public EnemyDefenderController enemyDefenderController;

    public RoadLine[] roadLines;

    public Transform cannonCardsDeckParent;
    public Transform[] defenderCannonParents;

    public DefenderCannonCode[] cannons;
    public DefenderCannonCode toplamaCannon;
    public DefenderCannonCode cikartmaCannon;
    public DefenderCannonCode carpmaCannon;
    public DefenderCannonCode bolmeCannon;

    public DefenderCannonCode currentSelectedCannon = null;

    public List<TrapUICardCode> enemysTrapDeck = new List<TrapUICardCode>();
    public Transform enemysTrapCardParent;
    public Transform enemysTrapObjectParent;

    void Start()
    {

    }

    public void GetEnemysDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            DefenderCannonCode defenderCannon = null;
            if(DataManager.Instance.enemyIsNonPlayer == 0)
            {
                if (i == 0)
                    defenderCannon = GameObject.FindGameObjectWithTag("toplamaCannon").GetComponent<DefenderCannonCode>();
                else if (i == 1)
                    defenderCannon = GameObject.FindGameObjectWithTag("cikartmaCannon").GetComponent<DefenderCannonCode>();
                else if (i == 2)
                    defenderCannon = GameObject.FindGameObjectWithTag("carpmaCannon").GetComponent<DefenderCannonCode>();
                else if (i == 3)
                    defenderCannon = GameObject.FindGameObjectWithTag("bolmeCannon").GetComponent<DefenderCannonCode>();
            }
            else
            {
                foreach (DefenderCannonCode defenderCannonPrefab in matchManager.defenderCannonPrefabs)
                {
                    if (defenderCannonPrefab.accessName == DataManager.Instance.enemysToplamaDefenderCannon.Split('_')[0]
                        || defenderCannonPrefab.accessName == DataManager.Instance.enemysCikartmaDefenderCannon.Split('_')[0]
                        || defenderCannonPrefab.accessName == DataManager.Instance.enemysCarpmaDefenderCannon.Split('_')[0]
                        || defenderCannonPrefab.accessName == DataManager.Instance.enemysBolmeDefenderCannon.Split('_')[0])
                    {
                        defenderCannon = Instantiate(defenderCannonPrefab, defenderCannonParents[i]);
                        defenderCannon.transform.localPosition = Vector3.zero;
                        defenderCannon.transform.localRotation = Quaternion.Euler(Vector3.zero);

                        string tag = "";
                        if (i == 0)
                            tag = "toplamaCannon";
                        else if (i == 1)
                            tag = "cikartmaCannon";
                        else if (i == 2)
                            tag = "carpmaCannon";
                        else if (i == 3)
                            tag = "bolmeCannon";

                        defenderCannon.gameObject.tag = tag;

                        break;
                    }
                }
            }


            if (defenderCannon != null)
            {
                if (i == 0)
                {
                    defenderCannon.starLevelValue = int.Parse(DataManager.Instance.enemysToplamaDefenderCannon.Split('|')[1]);
                    defenderCannon.classInfo = "toplama";
                    toplamaCannon = defenderCannon;
                    cannons[0] = defenderCannon;
                    roadLines[0].defenderCannon = defenderCannon;
                }
                else if (i == 1)
                {
                    defenderCannon.starLevelValue = int.Parse(DataManager.Instance.enemysCikartmaDefenderCannon.Split('|')[1]);
                    defenderCannon.classInfo = "cikartma";
                    cikartmaCannon = defenderCannon;
                    cannons[1] = defenderCannon;
                    roadLines[1].defenderCannon = defenderCannon;
                }
                else if (i == 2)
                {
                    defenderCannon.starLevelValue = int.Parse(DataManager.Instance.enemysCarpmaDefenderCannon.Split('|')[1]);
                    defenderCannon.classInfo = "carpma";
                    carpmaCannon = defenderCannon;
                    cannons[2] = defenderCannon;
                    roadLines[2].defenderCannon = defenderCannon;
                }
                else if (i == 3)
                {
                    defenderCannon.starLevelValue = int.Parse(DataManager.Instance.enemysBolmeDefenderCannon.Split('|')[1]);
                    defenderCannon.classInfo = "bolme";
                    bolmeCannon = defenderCannon;
                    cannons[3] = defenderCannon;
                    roadLines[3].defenderCannon = defenderCannon;
                }

                defenderCannon.SetReady();
                defenderCannon.targetSearch = true;
            }
            else
            {
                matchManager.RoundEnd();
            }
        }

        //string[] cardInfos = DataManager.Instance.enemysTrapDeckInfo.Split('#');

        //foreach (string cardInfo in cardInfos)
        //{
        //    if (cardInfo != "")
        //    {
        //        string cardID = cardInfo;
        //        string cardName = cardInfo.Split('_')[0];
        //        int cardStarLevel = int.Parse(cardID.Split('|')[1]);

        //        foreach (TrapUICardCode cardPrefab in matchManager.trapCardPrefabs)
        //        {
        //            TrapUICardCode card = null;
        //            if (cardPrefab.accessName == cardName)
        //            {
        //                card = Instantiate(cardPrefab, enemysTrapCardParent);
        //                card.ID = cardID;
        //                card.UpdateCardVariables();

        //                card.pointerOnDownEvent.AddListener(card.GamePointerOnDownFunction0);

        //                enemysTrapDeck.Add(card);
        //            }
        //        }
        //    }
        //}
    }

    public void PreparationCase()
    {
        if(enemyDefenderController.gameObject.activeSelf)
        {
            enemyDefenderController.SelectTrapsCase();
        }
        else
        {
            //TODO MULTIPLAYER
        }
    }

    public void RoundStarted()
    {
        if (enemyDefenderController.gameObject.activeSelf)
        {
            enemyDefenderController.StopAllCoroutines();
        }
        else
        {
            GetEnemysDeck();
            //TODO MULTIPLAYER
        }
    }

    void Update()
    {

    }

    public void ATrapPlacemented(string trapAccessName, Vector3 pos)
    {
        foreach(TrapObject trapObject in matchManager.trapObjectPrefabs)
        {
            if(trapObject.accessName == trapAccessName)
            {
                Instantiate(trapObject, pos, Quaternion.identity);
                break;
            }
        }
    }

    public void ACannonSelected(int cannonNo)
    {
        ACannonSelected(cannons[cannonNo]);
    }

    public void ACannonSelected(DefenderCannonCode targetCannon)
    {
        if (currentSelectedCannon == null)
        {
            currentSelectedCannon = targetCannon;
        }
    }

    public void QuestionAnswered(bool isCorrect)
    {
        if(currentSelectedCannon != null)
        {
            if (isCorrect)
            {
                currentSelectedCannon.ammoAmount++;
            }
            else
            {

            }
        }

        currentSelectedCannon = null;
    }
}
