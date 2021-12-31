using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    public bool isTest;

    public int roundNo = 0;
    public enum RoundStatuses
    {
        loading,
        inPreparation,
        started
    }
    public RoundStatuses roundStatus;

    public GameObject attackerGameSide;
    public GameObject defenderGameSide;

    public AttackerGameplayManager attackerGameplayManager;
    public DefenderGameplayManager defenderGameplayManager;

    public EnemyAttackerManager enemyAttackerManager;
    public EnemyDefenderManager enemyDefenderManager;

    public float timer;
    public TextMeshProUGUI timerText;

    public AttackerUICardCode[] attackerCardPrefabs;
    public AttackerCharacterCode[] attackerCharacterPrefabs;

    public DefenderCannonUICardCode[] defenderCannonCardPrefabs;
    public DefenderCannonCode[] defenderCannonPrefabs;

    public TrapUICardCode[] trapCardPrefabs;
    public TrapObject[] trapObjectPrefabs;


    [Header("StartUI")]
    public GameObject frontCanvas;
    public Sprite[] playerProfileSprites;
    public Image enemyProfileImage;
    public TextMeshProUGUI startEnemyUserNameText;
    public GameObject startPanel;
    public TextMeshProUGUI firstTimerText;
    public Image[] enemysClassAchivementInfoBars;

    [Header("AttackerSideResultUI")]
    public GameObject attackerSideResultPanel;
    public TextMeshProUGUI enemyDefenderHealthAmountText;
    public TextMeshProUGUI enemyDefenderUserNameText;
    public TextMeshProUGUI enemyDefenderLevelText;

    [Header("DefenderSideResultUI")]
    public GameObject defenderSideResultPanel;
    public TextMeshProUGUI playerDefenderHealthAmountText;
    public TextMeshProUGUI enemyAttackerUserNameText;
    public TextMeshProUGUI enemyAttackerLevelText;

    [Header("EnemyEscapedUI")]
    public GameObject enemyEscapedPanel;

    void Start()
    {
        //if (!isTest)
        //    SetReadyTheRound();
        //else
        //    PreparationCase();

        if(DataManager.Instance.enemyIsNonPlayer == 0)
            MainNetworkManager.Instance.matchManager = this;
        SetReadyTheRound();
    }

    public void SetReadyTheRound()
    {
        roundNo = DataManager.Instance.RoundNo;

        if (roundNo == 0)
        {
            if (DataManager.Instance.enemyIsNonPlayer == 0)
            {
                if (MainNetworkManager.Instance.player01.pw.IsMine)
                {
                    DataManager.Instance.enemysToplamaDefenderCannon = MainNetworkManager.Instance.player02.toplamaCannonID;
                    DataManager.Instance.enemysCikartmaDefenderCannon = MainNetworkManager.Instance.player02.cikartmaCannonID;
                    DataManager.Instance.enemysCarpmaDefenderCannon = MainNetworkManager.Instance.player02.carpmaCannonID;
                    DataManager.Instance.enemysBolmeDefenderCannon = MainNetworkManager.Instance.player02.bolmeCannonID;
                    DataManager.Instance.enemysAttackerGameDeckInfo = MainNetworkManager.Instance.player02.attackerGameDeckInfo;

                    DataManager.Instance.enemysProfileImageSpriteName = MainNetworkManager.Instance.player02.profileImageSpriteName;
                    DataManager.Instance.enemysUserName = MainNetworkManager.Instance.player02.userName;

                    DataManager.Instance.enemysTotalToplamaQuestionCount = MainNetworkManager.Instance.player02.totalToplamaQuestionCount;
                    DataManager.Instance.enemysTotalCikartmaQuestionCount = MainNetworkManager.Instance.player02.totalCikartmaQuestionCount;
                    DataManager.Instance.enemysTotalCarpmaQuestionCount = MainNetworkManager.Instance.player02.totalCarpmaQuestionCount;
                    DataManager.Instance.enemysTotalBolmeQuestionCount = MainNetworkManager.Instance.player02.totalBolmeQuestionCount;

                    DataManager.Instance.enemysCorrectAnsweredToplamaQuestionCount = MainNetworkManager.Instance.player02.correctAnsweredToplamaQuestionCount;
                    DataManager.Instance.enemysCorrectAnsweredCikartmaQuestionCount = MainNetworkManager.Instance.player02.correctAnsweredCikartmaQuestionCount;
                    DataManager.Instance.enemysCorrectAnsweredCarpmaQuestionCount = MainNetworkManager.Instance.player02.correctAnsweredCarpmaQuestionCount;
                    DataManager.Instance.enemysCorrectAnsweredBolmeQuestionCount = MainNetworkManager.Instance.player02.correctAnsweredBolmeQuestionCount;

                    DataManager.Instance.enemysLevel = MainNetworkManager.Instance.player02.level;
                }
                else
                {
                    DataManager.Instance.enemysToplamaDefenderCannon = MainNetworkManager.Instance.player01.toplamaCannonID;
                    DataManager.Instance.enemysCikartmaDefenderCannon = MainNetworkManager.Instance.player01.cikartmaCannonID;
                    DataManager.Instance.enemysCarpmaDefenderCannon = MainNetworkManager.Instance.player01.carpmaCannonID;
                    DataManager.Instance.enemysBolmeDefenderCannon = MainNetworkManager.Instance.player01.bolmeCannonID;
                    DataManager.Instance.enemysAttackerGameDeckInfo = MainNetworkManager.Instance.player01.attackerGameDeckInfo;

                    DataManager.Instance.enemysProfileImageSpriteName = MainNetworkManager.Instance.player01.profileImageSpriteName;
                    DataManager.Instance.enemysUserName = MainNetworkManager.Instance.player01.userName;

                    DataManager.Instance.enemysTotalToplamaQuestionCount = MainNetworkManager.Instance.player01.totalToplamaQuestionCount;
                    DataManager.Instance.enemysTotalCikartmaQuestionCount = MainNetworkManager.Instance.player01.totalCikartmaQuestionCount;
                    DataManager.Instance.enemysTotalCarpmaQuestionCount = MainNetworkManager.Instance.player01.totalCarpmaQuestionCount;
                    DataManager.Instance.enemysTotalBolmeQuestionCount = MainNetworkManager.Instance.player01.totalBolmeQuestionCount;

                    DataManager.Instance.enemysCorrectAnsweredToplamaQuestionCount = MainNetworkManager.Instance.player01.correctAnsweredToplamaQuestionCount;
                    DataManager.Instance.enemysCorrectAnsweredCikartmaQuestionCount = MainNetworkManager.Instance.player01.correctAnsweredCikartmaQuestionCount;
                    DataManager.Instance.enemysCorrectAnsweredCarpmaQuestionCount = MainNetworkManager.Instance.player01.correctAnsweredCarpmaQuestionCount;
                    DataManager.Instance.enemysCorrectAnsweredBolmeQuestionCount = MainNetworkManager.Instance.player01.correctAnsweredBolmeQuestionCount;

                    DataManager.Instance.enemysLevel = MainNetworkManager.Instance.player01.level;
                }
            }
        }

        if (DataManager.Instance.ThisRoundSide == "attacker")
        {
            attackerGameSide.gameObject.SetActive(true);

            attackerGameplayManager.gameObject.SetActive(true);
            timerText = enemyDefenderManager.timerText;

            if (DataManager.Instance.enemyIsNonPlayer == 1)
            {
                enemyDefenderManager.gameObject.SetActive(true);
                enemyDefenderManager.enemyDefenderController.gameObject.SetActive(true);

                enemyDefenderManager.GetEnemysDeck();
            }
            else
            {
                enemyDefenderManager.gameObject.SetActive(true);
                //TODO MULTIPLAYER MANAGER ACTIVE
            }
        }
        else if (DataManager.Instance.ThisRoundSide == "defender")
        {
            defenderGameSide.gameObject.SetActive(true);

            defenderGameplayManager.gameObject.SetActive(true);
            timerText = defenderGameplayManager.timerText;

            if (DataManager.Instance.enemyIsNonPlayer == 1)
            {
                enemyAttackerManager.gameObject.SetActive(true);
                enemyAttackerManager.enemyAttackerController.gameObject.SetActive(true);
            }
            else
            {
                enemyAttackerManager.gameObject.SetActive(true);
                //TODO MULTIPLAYER MANAGER ACTIVE
            }
        }

        PreparationCase();
    }

    public void PreparationCase()
    {
        frontCanvas.SetActive(true);
        startPanel.SetActive(true);
        foreach(Sprite sprite in playerProfileSprites)
        {
            if(sprite.name == DataManager.Instance.enemysProfileImageSpriteName)
            {
                enemyProfileImage.sprite = sprite;
                break;
            }
        }
        startEnemyUserNameText.text = DataManager.Instance.enemysUserName;
        enemyAttackerUserNameText.text = DataManager.Instance.enemysUserName;
        enemyDefenderUserNameText.text = DataManager.Instance.enemysUserName;

        enemyAttackerLevelText.text = DataManager.Instance.enemysLevel + " lv.";
        enemyDefenderLevelText.text = DataManager.Instance.enemysLevel + " lv.";

        //startPanel
        enemysClassAchivementInfoBars[0].fillAmount = DataManager.Instance.enemysCorrectAnsweredToplamaQuestionCount / DataManager.Instance.enemysTotalToplamaQuestionCount;
        enemysClassAchivementInfoBars[1].fillAmount = DataManager.Instance.enemysCorrectAnsweredCikartmaQuestionCount / DataManager.Instance.enemysTotalCikartmaQuestionCount;
        enemysClassAchivementInfoBars[2].fillAmount = DataManager.Instance.enemysCorrectAnsweredCarpmaQuestionCount / DataManager.Instance.enemysTotalCarpmaQuestionCount;
        enemysClassAchivementInfoBars[3].fillAmount = DataManager.Instance.enemysCorrectAnsweredBolmeQuestionCount / DataManager.Instance.enemysTotalBolmeQuestionCount;

        //attackerSide
        enemysClassAchivementInfoBars[4].fillAmount = DataManager.Instance.enemysCorrectAnsweredToplamaQuestionCount / DataManager.Instance.enemysTotalToplamaQuestionCount;
        enemysClassAchivementInfoBars[5].fillAmount = DataManager.Instance.enemysCorrectAnsweredCikartmaQuestionCount / DataManager.Instance.enemysTotalCikartmaQuestionCount;
        enemysClassAchivementInfoBars[6].fillAmount = DataManager.Instance.enemysCorrectAnsweredCarpmaQuestionCount / DataManager.Instance.enemysTotalCarpmaQuestionCount;
        enemysClassAchivementInfoBars[7].fillAmount = DataManager.Instance.enemysCorrectAnsweredBolmeQuestionCount / DataManager.Instance.enemysTotalBolmeQuestionCount;

        //defenderSide
        enemysClassAchivementInfoBars[8].fillAmount = DataManager.Instance.enemysCorrectAnsweredToplamaQuestionCount / DataManager.Instance.enemysTotalToplamaQuestionCount;
        enemysClassAchivementInfoBars[9].fillAmount = DataManager.Instance.enemysCorrectAnsweredCikartmaQuestionCount / DataManager.Instance.enemysTotalCikartmaQuestionCount;
        enemysClassAchivementInfoBars[10].fillAmount = DataManager.Instance.enemysCorrectAnsweredCarpmaQuestionCount / DataManager.Instance.enemysTotalCarpmaQuestionCount;
        enemysClassAchivementInfoBars[11].fillAmount = DataManager.Instance.enemysCorrectAnsweredBolmeQuestionCount / DataManager.Instance.enemysTotalBolmeQuestionCount;

        StartCoroutine(FirstDelay());
        IEnumerator FirstDelay()
        {
            float delay = 4.25f;

            while(delay > 0f && roundNo == 0)
            {
                firstTimerText.text = ((int)delay).ToString();
                delay -= 1f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            firstTimerText.text = "GO!";
            CloseStartPanel();

            timer = 30f;
            roundStatus = RoundStatuses.inPreparation;

            if (attackerGameplayManager.gameObject.activeSelf)
            {
                attackerGameplayManager.PreparationCase();
            }
            else if (enemyAttackerManager.gameObject.activeSelf)
            {
                enemyAttackerManager.PreparationCase();
            }

            if (defenderGameplayManager.gameObject.activeSelf)
            {
                defenderGameplayManager.PreparationCase();
            }
            else if (enemyDefenderManager.gameObject.activeSelf)
            {
                enemyDefenderManager.PreparationCase();
            }
        }
    }

    public void CloseStartPanel()
    {
        startPanel.SetActive(false);
        frontCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (roundStatus != RoundStatuses.loading)
        {
            timerText.text = ((int)(timer / 60f)).ToString() + ":" + ((int)(timer % 60f)).ToString();
            if (timer > 0f)
            {
                timer -= 1f * Time.deltaTime;
            }
            else if (timer < 0f)
            {
                if (roundStatus == RoundStatuses.inPreparation)
                {
                    PreparationCompleted();
                }
                else if (roundStatus == RoundStatuses.started)
                {
                    RoundEnd();
                }

                timer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(1))
            EnemyEscapedOkayButton();
    }

    public void PreparationCompleted()
    {
        roundStatus = RoundStatuses.started;

        if (attackerGameplayManager.gameObject.activeSelf)
        {
            attackerGameplayManager.RoundStarted();
        }
        else if (enemyAttackerManager.gameObject.activeSelf)
        {
            enemyAttackerManager.RoundStarted();
        }

        if (defenderGameplayManager.gameObject.activeSelf)
        {
            defenderGameplayManager.RoundStarted();
        }
        else if (enemyDefenderManager.gameObject.activeSelf)
        {
            enemyDefenderManager.RoundStarted();
        }

        timer = 60f * 1.5f;
    }

    float nextRoundDelay = 3f;
    public void RoundEnd()
    {
        roundStatus = RoundStatuses.loading;

        frontCanvas.SetActive(true);
        if (DataManager.Instance.ThisRoundSide == "attacker")
        {
            attackerSideResultPanel.SetActive(true);
            enemyDefenderUserNameText.text = DataManager.Instance.enemysUserName;
            enemyDefenderHealthAmountText.text = "%" + enemyDefenderManager.defenderTower.healthBarText.text;
            DataManager.Instance.lastAttackerSideResult = 100 - (int)enemyDefenderManager.defenderTower.healthAmount;
        }
        else if (DataManager.Instance.ThisRoundSide == "defender")
        {
            defenderSideResultPanel.SetActive(true);
            enemyAttackerUserNameText.text = DataManager.Instance.enemysUserName;
            playerDefenderHealthAmountText.text = "%" + defenderGameplayManager.defenderTower.healthBarText.text;
            DataManager.Instance.lastDefenderSideResult = (int)defenderGameplayManager.defenderTower.healthAmount;
        }

        if (roundNo >= 1)
        {
            //GAME END
        }
        else
        {
            nextRoundDelay = 3f;
            StartCoroutine(NextRoundDelay());
            IEnumerator NextRoundDelay()
            {
                while(nextRoundDelay > 0f)
                {
                    nextRoundDelay -= 1f * Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                //NEXT ROUND
                if (DataManager.Instance.ThisRoundSide == "attacker")
                {
                    foreach(DefenderCannonCode defenderCannon in enemyDefenderManager.cannons)
                    {
                        Destroy(defenderCannon.gameObject);
                    }
                    DataManager.Instance.ThisRoundSide = "defender";
                }
                else if (DataManager.Instance.ThisRoundSide == "defender")
                {
                    foreach (DefenderCannonCode defenderCannon in defenderGameplayManager.cannons)
                    {
                        Destroy(defenderCannon.gameObject);
                    }
                    DataManager.Instance.ThisRoundSide = "attacker";
                }

                roundNo++;
                DataManager.Instance.RoundNo = roundNo;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void SkipToNextRound()
    {
        nextRoundDelay = 0f;

        if(roundNo >= 1)
        {
            DataManager.Instance.ThisRoundSide = "";
            DataManager.Instance.RoundNo = 0;
            DataManager.Instance.lastResultReady = 1;

            if (DataManager.Instance.enemyIsNonPlayer == 0)
            {
                if (MainNetworkManager.Instance.player01 != null)
                {
                    if (!MainNetworkManager.Instance.player01.pw.IsMine)
                        Destroy(MainNetworkManager.Instance.player01.gameObject);
                    else
                        MainNetworkManager.Instance.LeaveRoom();
                }

                if (MainNetworkManager.Instance.player02 != null)
                {
                    if (!MainNetworkManager.Instance.player02.pw.IsMine)
                        Destroy(MainNetworkManager.Instance.player02.gameObject);
                    else
                        MainNetworkManager.Instance.LeaveRoom();
                }

                MainNetworkManager.Instance.LeaveLobby();
            }

            SceneManager.LoadScene(0);
        }
    }

    public void AttackerSpawnedANewCharacter(AttackerCharacterCode spawnedCharacter, RoadLine selectedRoad)
    {
        if (defenderGameplayManager.gameObject.activeSelf)
        {
            switch (selectedRoad.defenderCannon.classInfo)
            {
                case "toplama":
                    defenderGameplayManager.toplamaCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
                case "cikartma":
                    defenderGameplayManager.cikartmaCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
                case "carpma":
                    defenderGameplayManager.carpmaCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
                case "bolme":
                    defenderGameplayManager.bolmeCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
            }
        }
        else if (enemyDefenderManager.gameObject.activeSelf)
        {
            switch (selectedRoad.defenderCannon.classInfo)
            {
                case "toplama":
                    enemyDefenderManager.toplamaCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
                case "cikartma":
                    enemyDefenderManager.cikartmaCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
                case "carpma":
                    enemyDefenderManager.carpmaCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
                case "bolme":
                    enemyDefenderManager.bolmeCannon.attackersOnTheLine.Add(spawnedCharacter);
                    break;
            }

            if (enemyDefenderManager.enemyDefenderController.gameObject.activeSelf)
                enemyDefenderManager.enemyDefenderController.SelectACannon();
        }
    }

    public void EnemyEscaped()
    {
        frontCanvas.SetActive(true);
        enemyEscapedPanel.SetActive(true);
        roundNo = -1;
        roundStatus = RoundStatuses.loading;
    }

    public void EnemyEscapedOkayButton()
    {
        DataManager.Instance.Coin += 50;

        DataManager.Instance.ThisRoundSide = "";
        DataManager.Instance.RoundNo = 0;
        DataManager.Instance.lastResultReady = 0;

        if (DataManager.Instance.enemyIsNonPlayer == 0)
        {
            if (MainNetworkManager.Instance.player01 != null)
            {
                if (!MainNetworkManager.Instance.player01.pw.IsMine)
                    Destroy(MainNetworkManager.Instance.player01.gameObject);
                else
                    MainNetworkManager.Instance.LeaveRoom();
            }

            if (MainNetworkManager.Instance.player02 != null)
            {
                if (!MainNetworkManager.Instance.player02.pw.IsMine)
                    Destroy(MainNetworkManager.Instance.player02.gameObject);
                else
                    MainNetworkManager.Instance.LeaveRoom();
            }

            MainNetworkManager.Instance.LeaveLobby();
        }

        SceneManager.LoadScene(0);
    }

}
