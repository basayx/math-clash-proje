using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MatchResultManager : MonoBehaviour
{
    public MenuManager menuManager;
    public StoryManager storyManager;

    public GameObject resultPanel;
    public TextMeshProUGUI attackerSideResultText;
    public TextMeshProUGUI defenderSideResultText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI medalText;

    public Image[] classAchivementsInfoBar;

    void Start()
    {
        if(DataManager.Instance.lastResultReady == 1)
        {
            resultPanel.SetActive(true);

            classAchivementsInfoBar[0].fillAmount = DataManager.Instance.correctAnsweredToplamaQuestionCount / DataManager.Instance.totalToplamaQuestionCount;
            classAchivementsInfoBar[1].fillAmount = DataManager.Instance.correctAnsweredCikartmaQuestionCount / DataManager.Instance.totalCikartmaQuestionCount;
            classAchivementsInfoBar[2].fillAmount = DataManager.Instance.correctAnsweredCarpmaQuestionCount / DataManager.Instance.totalCarpmaQuestionCount;
            classAchivementsInfoBar[3].fillAmount = DataManager.Instance.correctAnsweredBolmeQuestionCount / DataManager.Instance.totalBolmeQuestionCount;

            attackerSideResultText.text = "% " + DataManager.Instance.lastAttackerSideResult;
            defenderSideResultText.text = "% " + DataManager.Instance.lastDefenderSideResult;

            int coinGainValue = (DataManager.Instance.lastAttackerSideResult / 5) + (DataManager.Instance.lastDefenderSideResult/ 5) + Random.Range(0, 10);
            int medalGainValue = (DataManager.Instance.lastAttackerSideResult / 10) + (DataManager.Instance.lastDefenderSideResult / 10) + Random.Range(0, 10);

            float expGainValue = (DataManager.Instance.lastAttackerSideResult / 10) + (DataManager.Instance.lastDefenderSideResult / 10) + Random.Range(0, 10);
            DataManager.Instance.Exp += expGainValue;
            if(DataManager.Instance.Exp >= 100f)
            {
                DataManager.Instance.Exp = 0f;
                DataManager.Instance.Level++;
                menuManager.levelText.text = DataManager.Instance.Level.ToString() + " lv.";
            }
            menuManager.expBarImage.DOFillAmount(DataManager.Instance.Exp / 100f, 0.75f);

            StartCoroutine(CoinGainCase(DataManager.Instance.Coin, DataManager.Instance.Coin + coinGainValue));
            DataManager.Instance.Coin += coinGainValue;

            DataManager.Instance.Medal += medalGainValue;
            menuManager.medalText.text = DataManager.Instance.Medal.ToString();

            coinText.text = "+" + coinGainValue.ToString();
            medalText.text = "+" + medalGainValue.ToString();

            DataManager.Instance.lastResultReady = 0;

            if(DataManager.Instance.LastPlayedStoryID != "")
            {
                if (DataManager.Instance.lastAttackerSideResult > 100 - DataManager.Instance.lastDefenderSideResult)
                {
                    DataManager.Instance.LastPlayedStoryWinStatus = 1;
                }
                else
                {
                    DataManager.Instance.LastPlayedStoryWinStatus = 0;
                }
                storyManager.GetLastStory();
            }
            else
            {
                DataManager.Instance.LastPlayedStoryWinStatus = 0;
            }
        }
    }

    IEnumerator CoinGainCase(int startValue, int endValue)
    {
        float value = startValue;
        while(value < endValue)
        {
            value += 75f * Time.deltaTime;
            menuManager.coinText.text = ((int)value).ToString();
            yield return new WaitForEndOfFrame();
        }
        menuManager.coinText.text = endValue.ToString();
    }

    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
    }
}
