using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public DeckManager deckManager;

    public GameObject backgroundPanel;
    public GameObject arenaPanel;
    public GameObject magazaPanel;
    public GameObject hikayePanel;
    public GameObject desteDuzenlemePanel;
    public GameObject kartDetayPanel;
    public GameObject kartDegisimPanel;
    [HideInInspector]
    public GameObject gerideKalanPanel;

    public Button arenaButton;
    public Button magazaButton;
    public Button karsilasmaAraButton;

    public TextMeshProUGUI userNameText;
    public Image profileImage;
    public Sprite[] profileImageSprites;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI medalText;
    public TextMeshProUGUI medalLeagueText;
    public Image medalLeagueImage;
    public Sprite[] medalSprites;

    public TextMeshProUGUI levelText;
    public Image expBarImage;

    public Animator canvasAnimator;
    public float matchSearchingTime = 0f;
    public TextMeshProUGUI matchSearchingTimeText;
    bool matchSearching = false;

    private void Start()
    {
        MainNetworkManager.Instance.menuManager = this;
        DataManager.Instance.RoundNo = 0;
        DataManager.Instance.ThisRoundSide = "attacker";
        UpdateVariables();
    }

    public void UpdateVariables()
    {
        userNameText.text = DataManager.Instance.playersUserName.ToString();
        foreach (Sprite profileSprite in profileImageSprites)
        {
            if (profileSprite.name == DataManager.Instance.playersProfileImageSpriteName)
            {
                profileImage.sprite = profileSprite;
                break;
            }
        }

        expBarImage.fillAmount = DataManager.Instance.Exp / 100f;
        levelText.text = DataManager.Instance.Level.ToString() + " lv.";

        coinText.text = DataManager.Instance.Coin.ToString();

        medalText.text = DataManager.Instance.Medal.ToString();
        if (DataManager.Instance.Medal >= ConstantVariables.medalCount_EfsanelerBirligi)
        {
            medalLeagueText.text = "Efsaneler Birliği";
            medalLeagueImage.sprite = medalSprites[5];
        }
        else if (DataManager.Instance.Medal >= ConstantVariables.medalCount_UstalarBirligi)
        {
            medalLeagueText.text = "Ustalar Birliği";
            medalLeagueImage.sprite = medalSprites[4];
        }
        else if (DataManager.Instance.Medal >= ConstantVariables.medalCount_KumandanBirligi)
        {
            medalLeagueText.text = "Kumandan Birliği";
            medalLeagueImage.sprite = medalSprites[3];
        }
        else if (DataManager.Instance.Medal >= ConstantVariables.medalCount_KolcuBirligi)
        {
            medalLeagueText.text = "Kolcu Birliği";
            medalLeagueImage.sprite = medalSprites[2];
        }
        else if (DataManager.Instance.Medal >= ConstantVariables.medalCount_AkinciBirligi)
        {
            medalLeagueText.text = "Akıncı Birliği";
            medalLeagueImage.sprite = medalSprites[1];
        }
        else
        {
            medalLeagueText.text = "Acemi Birliği";
            medalLeagueImage.sprite = medalSprites[0];
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && gerideKalanPanel != null)
        {
            PanellerArasindaGeriyeGec();
        }
    }

    public void PanellerArasindaGeriyeGec()
    {
        if (gerideKalanPanel == arenaPanel)
            ArenaPanelineGecButton();
        else if (gerideKalanPanel == magazaPanel)
            MagazaPanelineGecButton();
        else if (gerideKalanPanel == desteDuzenlemePanel && !deckManager.isInCardChange)
            DesteDuzenlemePanelineGecButton();
        else if (gerideKalanPanel == desteDuzenlemePanel && deckManager.isInCardChange)
            DesteDuzenlemePanelineGecButton(deckManager.inChangeTargetCard);
        else if (gerideKalanPanel == kartDetayPanel)
            KartDetayPanelineGecButton();
        else if (gerideKalanPanel == hikayePanel)
            HikayePanelineGecButton();
    }

    public void ArenaPanelineGecButton()
    {
        if (matchSearching)
            return;

        backgroundPanel.SetActive(true);
        arenaPanel.SetActive(true);
        magazaPanel.SetActive(false);
        hikayePanel.SetActive(false);
        desteDuzenlemePanel.SetActive(false);
        kartDetayPanel.SetActive(false);
        kartDegisimPanel.SetActive(false);

        gerideKalanPanel = arenaPanel;
    }

    public void MagazaPanelineGecButton()
    {
        if (matchSearching)
            return;

        backgroundPanel.SetActive(true);
        magazaPanel.SetActive(true);
        arenaPanel.SetActive(false);
        hikayePanel.SetActive(false);
        desteDuzenlemePanel.SetActive(false);
        kartDetayPanel.SetActive(false);
        kartDegisimPanel.SetActive(false);

        gerideKalanPanel = arenaPanel;
    }

    public void HikayePanelineGecButton()
    {
        if (matchSearching)
            return;

        backgroundPanel.SetActive(false);
        hikayePanel.SetActive(true);
        magazaPanel.SetActive(false);
        arenaPanel.SetActive(false);
        desteDuzenlemePanel.SetActive(false);
        kartDetayPanel.SetActive(false);
        kartDegisimPanel.SetActive(false);

        gerideKalanPanel = arenaPanel;
    }

    public void DesteDuzenlemePanelineGecButton()
    {
        if (matchSearching)
            return;

        deckManager.UpdateCardLists();

        backgroundPanel.SetActive(true);
        desteDuzenlemePanel.SetActive(true);
        arenaPanel.SetActive(false);
        magazaPanel.SetActive(false);
        hikayePanel.SetActive(false);
        kartDetayPanel.SetActive(false);
        kartDegisimPanel.SetActive(false);

        deckManager.scrollbar.onValueChanged.Invoke(1f);
        gerideKalanPanel = arenaPanel;
    }

    public void DesteDuzenlemePanelineGecButton(UICardCode inChangeCard)
    {
        if (matchSearching)
            return;

        deckManager.CloseCardChangePanel();
        if(inChangeCard != null)
        {
            if (inChangeCard.GetComponent<AttackerCharacterCode>())
                deckManager.OpenChangePanelToThisCard(inChangeCard.GetComponent<AttackerUICardCode>());
            else if (inChangeCard.GetComponent<DefenderCannonUICardCode>())
                deckManager.OpenChangePanelToThisCard(inChangeCard.GetComponent<DefenderCannonUICardCode>());
        }

        backgroundPanel.SetActive(true);
        desteDuzenlemePanel.SetActive(true);
        arenaPanel.SetActive(false);
        magazaPanel.SetActive(false);
        hikayePanel.SetActive(false);
        kartDetayPanel.SetActive(false);
        //kartDegisimPanel.SetActive(false);

        gerideKalanPanel = arenaPanel;
    }

    public void KartDetayPanelineGecButton()
    {
        if (matchSearching)
            return;

        backgroundPanel.SetActive(true);
        kartDetayPanel.SetActive(true);
        arenaPanel.SetActive(false);
        magazaPanel.SetActive(false);
        hikayePanel.SetActive(false);
        desteDuzenlemePanel.SetActive(false);
        kartDegisimPanel.SetActive(false);

        gerideKalanPanel = desteDuzenlemePanel;
    }

    public void KartDegisimPanelineGecButton()
    {
        if (matchSearching)
            return;

        backgroundPanel.SetActive(true);
        kartDegisimPanel.SetActive(true);
        arenaPanel.SetActive(false);
        magazaPanel.SetActive(false);
        hikayePanel.SetActive(false);
        //desteDuzenlemePanel.SetActive(false);
        kartDetayPanel.SetActive(false);

        gerideKalanPanel = desteDuzenlemePanel;
    }

    public void KarsilasmaAraButton()
    {
        if (matchSearching)
            return;

        DataManager.Instance.enemyIsNonPlayer = 0;
        MainNetworkManager.Instance.QuickMatch();
        canvasAnimator.SetTrigger("matchFindCase");
        matchSearchingTime = 0f;
        matchSearching = true;
        StartCoroutine(MatchSearching());
    }

    IEnumerator MatchSearching()
    {
        while (matchSearching)
        {
            matchSearchingTime += 1f * Time.deltaTime;
            matchSearchingTimeText.text = ((int)(matchSearchingTime / 60f)).ToString() + ":" + ((int)(matchSearchingTime % 60f)).ToString();
            yield return new WaitForEndOfFrame();
        }
    }

    public void KarsilasmaAramaIptalButton()
    {
        if (!matchSearching)
            return;

        canvasAnimator.SetTrigger("matchFindCancel");

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

        matchSearching = false;
    }
}
