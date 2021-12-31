using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public bool InTutorial = false;
    public StoryManager StoryManager;
    public GameObject TutorialPanel;
    public GameObject[] GameplayTutorialPanels;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void FirstTimeTutorialCheck()
    {
        if (DataManager.Instance.TutorialCompleted == 0)
        {
            InTutorial = true;
            StoryManager.StartThisStory(StoryManager.storyParts[0]);
        }
    }

    public void GameplayTutorial()
    {
        TutorialPanel.SetActive(true);
    }

    public void NextPanelButton(int no)
    {
        if (no >= GameplayTutorialPanels.Length)
        {
            InTutorial = false;
            DataManager.Instance.TutorialCompleted = 1;
            StoryManager.PlayThisStory(StoryManager.storyParts[0]);
        }
        else
        {
            for (int i = 0; i < GameplayTutorialPanels.Length; i++)
            {
                if (i == no)
                    GameplayTutorialPanels[i].SetActive(true);
                else
                    GameplayTutorialPanels[i].SetActive(false);
            }
        }
    }
}
