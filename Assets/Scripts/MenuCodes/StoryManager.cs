using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject MainCanvas;
    public GameObject StroyMapGroup;
    public GameObject StoryGroup;
    public StoryPart[] storyParts;
    public StoryPart CurrentStoryPart;

    int talkNo = 0;
    bool isAfter = false;

    [Header("Stroy Talk")]
    public TextMeshProUGUI InTalkPersonNameText;
    public TextMeshProUGUI TalkText;
    public Transform CharactersParent;
    public Transform BackgroundsParent;
    public Transform IconsParent;

    public void GetLastStory()
    {
        if (DataManager.Instance.LastPlayedStoryID != "")
        {
            foreach (StoryPart storyPart in storyParts)
            {
                if (storyPart.stroyID == DataManager.Instance.LastPlayedStoryID)
                {
                    CurrentStoryPart = storyPart;
                    break;
                }
            }

            DataManager.Instance.LastPlayedStoryID = "";
            if (CurrentStoryPart != null && ((DataManager.Instance.LastPlayedStoryWinStatus == 1 && CurrentStoryPart.AfterWinStoryTalk.Count > 0) || (DataManager.Instance.LastPlayedStoryWinStatus == 0 && CurrentStoryPart.AfterLoseStoryTalk.Count > 0)))
            {
                StartThisStory(CurrentStoryPart);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && StoryGroup.activeSelf && !isAfter)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartThisStory(StoryPart storyPart)
    {
        if (PlayerPrefs.GetInt(storyPart.stroyID + "_Completed") == 1)
            return;

        StoryGroup.gameObject.SetActive(true);
        StroyMapGroup.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(false);

        if(CurrentStoryPart != storyPart)
        {
            CurrentStoryPart = storyPart;
            if (CurrentStoryPart.BeforeStoryTalk.Count > 0)
            {
                talkNo = 0;
                isAfter = false;
                StroyNextTalk();
            }
            else
            {
                PlayThisStory(CurrentStoryPart);
            }
        }
        else
        {
            talkNo = 0;
            isAfter = true;
            StroyNextTalk();
        }
    }

    public void StroyNextTalk()
    {
        StoryPart.StoryTalk storyTalk = new StoryPart.StoryTalk(true);
        if (isAfter)
        {
            if(DataManager.Instance.LastPlayedStoryWinStatus == 1)
            {
                if (CurrentStoryPart.AfterWinStoryTalk.Count > talkNo)
                    storyTalk = new StoryPart.StoryTalk(CurrentStoryPart.AfterWinStoryTalk[talkNo]);
                else
                {
                    PlayerPrefs.SetInt(CurrentStoryPart.stroyID + "_Completed", 1);
                    DataManager.Instance.LastPlayedStoryWinStatus = 0;
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                if (CurrentStoryPart.AfterLoseStoryTalk.Count > talkNo)
                    storyTalk = new StoryPart.StoryTalk(CurrentStoryPart.AfterLoseStoryTalk[talkNo]);
                else
                    SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (CurrentStoryPart.BeforeStoryTalk.Count > talkNo)
                storyTalk = new StoryPart.StoryTalk(CurrentStoryPart.BeforeStoryTalk[talkNo]);
            else if (TutorialManager.Instance.InTutorial == false)
                PlayThisStory(CurrentStoryPart);
            else
                TutorialManager.Instance.GameplayTutorial();
        }

        if(storyTalk.InTalkPersonName != "")
        {
            InTalkPersonNameText.text = storyTalk.InTalkPersonName;
            string talkText = storyTalk.TalkText.Replace("%USERNAME%", "<b>" + DataManager.Instance.playersUserName + "</b>");
            TalkText.text = talkText;

            foreach (Transform child in CharactersParent)
            {
                if (child.gameObject.name == storyTalk.InTalkPersonID)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }

            foreach (Transform child in BackgroundsParent)
            {
                if (child.gameObject.name == storyTalk.BackgroundID)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }

            foreach (Transform child in IconsParent)
            {
                if (storyTalk.IconsID.Contains(child.gameObject.name))
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }

        talkNo++;
    }

    public void PlayThisStory(StoryPart storyPart)
    {
        DataManager.Instance.LastPlayedStoryID = storyPart.stroyID;

        DataManager.Instance.enemysToplamaDefenderCannon = storyPart.toplamaCannonID;
        DataManager.Instance.enemysCikartmaDefenderCannon = storyPart.cikartmaCannonID;
        DataManager.Instance.enemysCarpmaDefenderCannon = storyPart.carpmaCannonID;
        DataManager.Instance.enemysBolmeDefenderCannon = storyPart.bolmeCannonID;
        DataManager.Instance.enemysAttackerGameDeckInfo = storyPart.attackerGameDeckInfo;

        DataManager.Instance.enemysProfileImageSpriteName = storyPart.profileImageSpriteName;
        DataManager.Instance.enemysUserName = storyPart.userName;

        DataManager.Instance.enemysTotalToplamaQuestionCount = storyPart.totalToplamaQuestionCount;
        DataManager.Instance.enemysTotalCikartmaQuestionCount = storyPart.totalCikartmaQuestionCount;
        DataManager.Instance.enemysTotalCarpmaQuestionCount = storyPart.totalCarpmaQuestionCount;
        DataManager.Instance.enemysTotalBolmeQuestionCount = storyPart.totalBolmeQuestionCount;

        DataManager.Instance.enemysCorrectAnsweredToplamaQuestionCount = storyPart.correctAnsweredToplamaQuestionCount;
        DataManager.Instance.enemysCorrectAnsweredCikartmaQuestionCount = storyPart.correctAnsweredCikartmaQuestionCount;
        DataManager.Instance.enemysCorrectAnsweredCarpmaQuestionCount = storyPart.correctAnsweredCarpmaQuestionCount;
        DataManager.Instance.enemysCorrectAnsweredBolmeQuestionCount = storyPart.correctAnsweredBolmeQuestionCount;

        DataManager.Instance.enemysAttackerGameDeckInfo = storyPart.attackerGameDeckInfo;

        DataManager.Instance.enemysLevel = storyPart.level;

        DataManager.Instance.ThisRoundSide = storyPart.playerThisRoundSide;
        DataManager.Instance.enemyIsNonPlayer = 1;

        SceneManager.LoadScene(1);
    }
}
