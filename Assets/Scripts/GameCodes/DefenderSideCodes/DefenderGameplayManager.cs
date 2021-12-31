using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefenderGameplayManager : MonoBehaviour
{
    public MatchManager matchManager;
    public TextMeshProUGUI timerText;

    public DefenderTower defenderTower;

    public Transform trapCardsParent;
    public List<TrapUICardCode> playersTrapDeck = new List<TrapUICardCode>();

    public RoadLine[] roadLines;

    public TrapUICardCode selectedTrapCard = null;
    public Transform trapDragObject;
    public Transform trapObjectsParent;

    public int trapAreaCount = 0;
    public int maxTrapAreaCount = 5;

    public Transform cannonCardsDeckParent;
    public Transform[] defenderCannonParents;

    public DefenderCannonCode[] cannons;
    public DefenderCannonCode toplamaCannon;
    public DefenderCannonCode cikartmaCannon;
    public DefenderCannonCode carpmaCannon;
    public DefenderCannonCode bolmeCannon;

    public DefenderCannonCode currentSelectedCannon = null;

    [Header("UI")]
    public GameObject trapBuildPanel;
    public GameObject gamePanel;
    public TextMeshProUGUI trapAreaCountText;

    public GameObject cannonsPanel;
    public DefenderCannonUIGroup[] cannonUIGroups;

    public GameObject questionPanel;
    public Question currentQuestion;
    public int currentQuestionAnswerNo;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerTexts;
    public Image[] answerButtonImages;
    public Color32[] answerButtonImageColors;

    void Start()
    {
        GetPlayersDeck();
    }

    public void GetPlayersDeck()
    {
        DefenderCannonUICardCode[] cannonCards = new DefenderCannonUICardCode[4];
        foreach (DefenderCannonUICardCode cannonCard in matchManager.defenderCannonCardPrefabs)
        {
            if (cannonCard.accessName == DataManager.Instance.playersToplamaDefenderCannon.Split('_')[0])
            {
                cannonCards[0] = Instantiate(cannonCard, cannonCardsDeckParent);
                cannonCards[0].ID = DataManager.Instance.playersToplamaDefenderCannon;
            }
            if (cannonCard.accessName == DataManager.Instance.playersCikartmaDefenderCannon.Split('_')[0])
            {
                cannonCards[1] = Instantiate(cannonCard, cannonCardsDeckParent);
                cannonCards[1].ID = DataManager.Instance.playersCikartmaDefenderCannon;
            }
            if (cannonCard.accessName == DataManager.Instance.playersCarpmaDefenderCannon.Split('_')[0])
            {
                cannonCards[2] = Instantiate(cannonCard, cannonCardsDeckParent);
                cannonCards[2].ID = DataManager.Instance.playersCarpmaDefenderCannon;
            }
            if (cannonCard.accessName == DataManager.Instance.playersBolmeDefenderCannon.Split('_')[0])
            {
                cannonCards[3] = Instantiate(cannonCard, cannonCardsDeckParent);
                cannonCards[3].ID = DataManager.Instance.playersBolmeDefenderCannon;
            }

            if (cannonCards[0] != null && cannonCards[1] != null && cannonCards[2] != null && cannonCards[3] != null)
                break;
        }

        for(int i = 0; i < 4; i++)
        {
            cannonCards[i].UpdateCardVariables();
            DefenderCannonCode defenderCannon = null;
            foreach (DefenderCannonCode defenderCannonPrefab in matchManager.defenderCannonPrefabs)
            {
                if(defenderCannonPrefab.accessName == cannonCards[i].accessName)
                {
                    GameObject targetPos = new GameObject();
                    targetPos.transform.SetParent(defenderCannonParents[i]);
                    targetPos.transform.localPosition = Vector3.zero;
                    targetPos.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    if(DataManager.Instance.enemyIsNonPlayer == 0)
                        defenderCannon = Photon.Pun.PhotonNetwork.Instantiate(defenderCannonPrefab.accessName, targetPos.transform.position, targetPos.transform.rotation, 0, null).gameObject.GetComponent<DefenderCannonCode>();
                    else
                        defenderCannon = Instantiate(defenderCannonPrefab, targetPos.transform.position, targetPos.transform.rotation);
                    string tag = "";
                    if (i == 0)
                        tag = "toplamaCannon";
                    else if(i == 1)
                        tag = "cikartmaCannon";
                    else if (i == 2)
                        tag = "carpmaCannon";
                    else if (i == 3)
                        tag = "bolmeCannon";
                    if (DataManager.Instance.enemyIsNonPlayer == 0)
                        defenderCannon.gameObject.GetComponent<Photon.Pun.PhotonView>().RPC("SetTag", Photon.Pun.RpcTarget.AllBuffered, tag);
                    else
                        defenderCannon.tag = tag;

                    break;
                }
            }

            if(defenderCannon != null)
            {
                if (i == 0)
                {
                    defenderCannon.starLevelValue = cannonCards[0].starLevelValue;
                    defenderCannon.cannonUIGroup = cannonUIGroups[0];
                    defenderCannon.classInfo = "toplama";
                    toplamaCannon = defenderCannon;
                    cannons[0] = defenderCannon;
                    roadLines[0].defenderCannon = defenderCannon;
                }
                else if (i == 1)
                {
                    defenderCannon.starLevelValue = cannonCards[1].starLevelValue;
                    defenderCannon.cannonUIGroup = cannonUIGroups[1];
                    defenderCannon.classInfo = "cikartma";
                    cikartmaCannon = defenderCannon;
                    cannons[1] = defenderCannon;
                    roadLines[1].defenderCannon = defenderCannon;
                }
                else if (i == 2)
                {
                    defenderCannon.starLevelValue = cannonCards[2].starLevelValue;
                    defenderCannon.cannonUIGroup = cannonUIGroups[2];
                    defenderCannon.classInfo = "carpma";
                    carpmaCannon = defenderCannon;
                    cannons[2] = defenderCannon;
                    roadLines[2].defenderCannon = defenderCannon;
                }
                else if (i == 3)
                {
                    defenderCannon.starLevelValue = cannonCards[3].starLevelValue;
                    defenderCannon.cannonUIGroup = cannonUIGroups[3];
                    defenderCannon.classInfo = "bolme";
                    bolmeCannon = defenderCannon;
                    cannons[3] = defenderCannon;
                    roadLines[3].defenderCannon = defenderCannon;
                }

                defenderCannon.SetReady();
                defenderCannon.targetSearch = true;
            }
        }

        string[] cardInfos = DataManager.Instance.playersTrapDeckInfo.Split('#');

        foreach (string cardInfo in cardInfos)
        {
            if (cardInfo != "")
            {
                string cardID = cardInfo;
                string cardName = cardInfo.Split('_')[0];
                int cardStarLevel = int.Parse(cardID.Split('|')[1]);

                foreach (TrapUICardCode cardPrefab in matchManager.trapCardPrefabs)
                {
                    TrapUICardCode card = null;
                    if (cardPrefab.accessName == cardName)
                    {
                        card = Instantiate(cardPrefab, trapCardsParent);
                        card.ID = cardID;
                        card.UpdateCardVariables();
                        card.gameplayManager = this;
                        card.pointerOnDownEvent.AddListener(card.GamePointerOnDownFunction0);

                        playersTrapDeck.Add(card);
                        break;
                    }
                }
            }
        }

        //foreach (TrapUICardCode card in playersTrapDeck)
        //{
        //    TrapUICardCode trapCard = Instantiate(card, trapCardsParent);
        //    trapCard.gameplayManager = this;
        //    trapCard.pointerOnDownEvent.AddListener(trapCard.GamePointerOnDownFunction0);
        //}
    }

    public void PreparationCase()
    {
        trapAreaCount = 0;
        trapAreaCountText.text = trapAreaCount.ToString() + "/" + maxTrapAreaCount.ToString();
        trapBuildPanel.SetActive(true);
    }

    public void RoundStarted()
    {
        UnSelectedTheTrapCard();
        trapBuildPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void Update()
    {
        if (selectedTrapCard != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<RoadLine>())
                    {
                        trapDragObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.GetComponent<RoadLine>().potentialTrapPoses[0].position.y, hit.point.z);

                        if (Input.GetMouseButtonUp(0))
                        {
                            if (selectedTrapCard.classInfo.Contains(hit.collider.gameObject.GetComponent<RoadLine>().defenderCannon.classInfo))
                            {
                                PlacementedATrapObject();
                            }
                            else
                            {
                                UnSelectedTheTrapCard();
                            }
                        }
                    }
                    else
                    {
                        trapDragObject.transform.position = new Vector3(hit.point.x, trapDragObject.transform.position.y, hit.point.z);

                        if (Input.GetMouseButtonUp(0))
                        {
                            UnSelectedTheTrapCard();
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    UnSelectedTheTrapCard();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                UnSelectedTheTrapCard();
            }
        }
    }

    public void SelectACannonButton(int cannonNo)
    {
        ACannonSelected(cannons[cannonNo]);
    }

    public void ACannonSelected(DefenderCannonCode targetCannon)
    {
        if(currentSelectedCannon == null && targetCannon.ammoAmount < targetCannon.maxAmmoAmount)
        {
            currentSelectedCannon = targetCannon;
            GetAnQuestion(targetCannon.classInfo);
        }
    }

    public void ATrapCardSelected(TrapUICardCode card)
    {
        selectedTrapCard = card;
        trapDragObject = Instantiate(card.viewObjectPrefab.gameObject).transform;
        Destroy(trapDragObject.gameObject.GetComponent<Collider>());

        foreach (RoadLine roadLine in roadLines)
        {
            if (selectedTrapCard.classInfo.Contains(roadLine.defenderCannon.classInfo))
            {
                roadLine.gameObject.SetActive(true);
            }
        }
    }

    public void UnSelectedTheTrapCard()
    {
        selectedTrapCard = null;

        foreach (RoadLine roadLine in roadLines)
        {
            roadLine.gameObject.SetActive(false);
        }

        if (trapDragObject != null)
            Destroy(trapDragObject.gameObject);
    }

    public void PlacementedATrapObject()
    {
        if(trapAreaCount + selectedTrapCard.areaCount <= maxTrapAreaCount)
        {
            trapAreaCount += selectedTrapCard.areaCount;
            trapAreaCountText.text = trapAreaCount.ToString() + "/" + maxTrapAreaCount.ToString();

            TrapObject trapObject = null;
            foreach (TrapObject trap in matchManager.trapObjectPrefabs)
            {
                if (trap.accessName == selectedTrapCard.accessName)
                {
                    trapObject = trap;
                    break;
                }
            }
            Instantiate(trapObject, trapObjectsParent).transform.position = trapDragObject.transform.position;

            Destroy(selectedTrapCard.gameObject);
        }

        if(DataManager.Instance.enemyIsNonPlayer == 0)
        {
            if (MainNetworkManager.Instance.player01.pw.IsMine)
                MainNetworkManager.Instance.player01.pw.RPC("ATrapPlacemented", Photon.Pun.RpcTarget.AllBuffered, selectedTrapCard.accessName, trapDragObject.transform.position);
            else if (MainNetworkManager.Instance.player02.pw.IsMine)
                MainNetworkManager.Instance.player02.pw.RPC("ATrapPlacemented", Photon.Pun.RpcTarget.AllBuffered, selectedTrapCard.accessName, trapDragObject.transform.position);
        }

        Destroy(trapDragObject.gameObject);
        UnSelectedTheTrapCard();
        if(trapAreaCount >= maxTrapAreaCount || trapCardsParent.childCount <= 1)
        {
            ComplateThePreparation();
        }
    }

    public void ComplateThePreparation()
    {
        if (DataManager.Instance.enemyIsNonPlayer == 0)
        {
            if (MainNetworkManager.Instance.player01.pw.IsMine)
                MainNetworkManager.Instance.player01.pw.RPC("DefenderPreparationCompleted", Photon.Pun.RpcTarget.AllBuffered, null);
            else if (MainNetworkManager.Instance.player02.pw.IsMine)
                MainNetworkManager.Instance.player02.pw.RPC("DefenderPreparationCompleted", Photon.Pun.RpcTarget.AllBuffered, null);
        }

        matchManager.PreparationCompleted();
    }

    public void GetAnQuestion(string typeName)
    {
        for(int i = 0; i < answerButtonImages.Length; i++)
        {
            answerButtonImages[i].color = answerButtonImageColors[0];
        }

        Question questionObj = QuestionManager.Instance.GetAnQuestion(typeName);

        questionText.text = questionObj.question;
        currentQuestionAnswerNo = Random.Range(0, 4);
        for(int i = 0; i < 4; i++)
        {
            if (i != currentQuestionAnswerNo)
            {
                int randomAnswer = Random.Range(questionObj.answer - 11, questionObj.answer + 11);
                if (randomAnswer == questionObj.answer)
                    randomAnswer += Random.Range(1, 3);

                answerTexts[i].text = randomAnswer.ToString();

            }
            else
                answerTexts[i].text = questionObj.answer.ToString();
        }

        questionPanel.SetActive(true);
        cannonsPanel.SetActive(false);
    }

    public void AnswerTheQuestion(int answerNo)
    {
        if (answerNo == currentQuestionAnswerNo)
        {
            QuestionAnswered(true);
            answerButtonImages[answerNo].color = answerButtonImageColors[1];
        }
        else
        {
            QuestionAnswered(false);
            answerButtonImages[answerNo].color = answerButtonImageColors[2];
            answerButtonImages[currentQuestionAnswerNo].color = answerButtonImageColors[3];
        }
    }

    public void QuestionAnswered(bool isCorrect)
    {
        if (isCorrect)
        {
            if (DataManager.Instance.enemyIsNonPlayer == 0)
                currentSelectedCannon.gameObject.GetComponent<Photon.Pun.PhotonView>().RPC("AmmoIncreased", Photon.Pun.RpcTarget.AllBuffered, null);
            else if (DataManager.Instance.enemyIsNonPlayer == 1)
                currentSelectedCannon.ammoAmount++;
            currentSelectedCannon.cannonUIGroup.AmmoIncrease(currentSelectedCannon.ammoAmount);
            StartCoroutine(Delay(0.4f));
        }
        else
        {
            StartCoroutine(Delay(1.8f));
        }

        currentSelectedCannon = null;

        IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            questionPanel.SetActive(false);
            cannonsPanel.SetActive(true);
        }
    }
}
