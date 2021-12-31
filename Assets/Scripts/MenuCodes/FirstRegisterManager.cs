using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstRegisterManager : MonoBehaviour
{
    public MenuManager menuManager;
    public GameObject registerGroup;

    public GameObject registerPanel;
    public TMP_InputField userNameInput;

    public GameObject profileSelectPanel;

    void Start()
    {
        if (DataManager.Instance.FirstTime == 1)
        {
            registerGroup.SetActive(true);
        }
        else
        {
            if (DataManager.Instance.TutorialCompleted == 0)
                TutorialManager.Instance.FirstTimeTutorialCheck();
            Destroy(registerGroup);
            Destroy(gameObject);
        }
    }

    public void Register()
    {
        DataManager.Instance.playersUserName = userNameInput.text;
        registerPanel.SetActive(false);
        profileSelectPanel.SetActive(true);
    }

    public void SelectProfile(string profileImageName)
    {
        DataManager.Instance.playersProfileImageSpriteName = profileImageName;
        DataManager.Instance.FirstTime = 0;
        Destroy(registerGroup);
        Destroy(gameObject);
        menuManager.UpdateVariables();
        TutorialManager.Instance.FirstTimeTutorialCheck();
    }
}
