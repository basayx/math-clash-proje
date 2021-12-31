using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public bool Sound
    {
        get => PlayerPrefs.GetInt("Sound", 1) == 1;
        set => PlayerPrefs.SetInt("Sound", value ? 1 : 0);
    }

    public int FirstTime
    {
        get => PlayerPrefs.GetInt("FirstTime", 1);
        set => PlayerPrefs.SetInt("FirstTime", value);
    }

    public int TutorialCompleted
    {
        get => PlayerPrefs.GetInt("TutorialCompleted", 0);
        set => PlayerPrefs.SetInt("TutorialCompleted", value);
    }

    public int Level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }

    public float Exp
    {
        get => PlayerPrefs.GetFloat("Exp", 0);
        set => PlayerPrefs.SetFloat("Exp", value);
    }

    public int Coin
    {
        get => PlayerPrefs.GetInt("Coin", 0);
        set => PlayerPrefs.SetInt("Coin", value);
    }

    public int Medal
    {
        get => PlayerPrefs.GetInt("Medal", 0);
        set => PlayerPrefs.SetInt("Medal", value);
    }

    public int RoundNo
    {
        get => PlayerPrefs.GetInt("RoundNo", 0);
        set => PlayerPrefs.SetInt("RoundNo", value);
    }

    public string ThisRoundSide
    {
        get => PlayerPrefs.GetString("ThisRoundSide", "attacker");
        set => PlayerPrefs.SetString("ThisRoundSide", value);
    }

    public int enemyIsNonPlayer
    {
        get => PlayerPrefs.GetInt("enemyIsNonPlayer", 1);
        set => PlayerPrefs.SetInt("enemyIsNonPlayer", value);
    }

    public int enemysLevel
    {
        get => PlayerPrefs.GetInt("enemysLevel", 0);
        set => PlayerPrefs.SetInt("enemysLevel", value);
    }

    public string playersAttackerDeckInfo
    {
        get => PlayerPrefs.GetString("playersAttackerDeckInfo", "");
        set => PlayerPrefs.SetString("playersAttackerDeckInfo", value);
    }
    public string playersAttackerGameDeckInfo
    {
        get => PlayerPrefs.GetString("playersAttackerGameDeckInfo", "ustaJiron_0|0#teymenLinda_1|0#eskiKurtLuke_0|0#trozanSavascisi_0|0#");
        set => PlayerPrefs.SetString("playersAttackerGameDeckInfo", value);
    }
    public string enemysAttackerGameDeckInfo
    {
        get => PlayerPrefs.GetString("enemysAttackerGameDeckInfo", "");
        set => PlayerPrefs.SetString("enemysAttackerGameDeckInfo", value);
    }

    public string playersDefenderCannonDeckInfo
    {
        get => PlayerPrefs.GetString("playersDefenderCannonDeckInfo", "");
        set => PlayerPrefs.SetString("playersDefenderCannonDeckInfo", value);
    }
    public string playersToplamaDefenderCannon
    {
        get => PlayerPrefs.GetString("playersToplamaDefenderCannon", "temelTipCannon_0|0");
        set => PlayerPrefs.SetString("playersToplamaDefenderCannon", value);
    }
    public string playersCikartmaDefenderCannon
    {
        get => PlayerPrefs.GetString("playersCikartmaDefenderCannon", "temelTipCannon_1|0");
        set => PlayerPrefs.SetString("playersCikartmaDefenderCannon", value);
    }
    public string playersCarpmaDefenderCannon
    {
        get => PlayerPrefs.GetString("playersCarpmaDefenderCannon", "temelTipCannon_2|0");
        set => PlayerPrefs.SetString("playersCarpmaDefenderCannon", value);
    }
    public string playersBolmeDefenderCannon
    {
        get => PlayerPrefs.GetString("playersBolmeDefenderCannon", "temelTipCannon_3|0");
        set => PlayerPrefs.SetString("playersBolmeDefenderCannon", value);
    }

    public string enemysToplamaDefenderCannon
    {
        get => PlayerPrefs.GetString("enemysToplamaDefenderCannon", "");
        set => PlayerPrefs.SetString("enemysToplamaDefenderCannon", value);
    }
    public string enemysCikartmaDefenderCannon
    {
        get => PlayerPrefs.GetString("enemysCikartmaDefenderCannon", "");
        set => PlayerPrefs.SetString("enemysCikartmaDefenderCannon", value);
    }
    public string enemysCarpmaDefenderCannon
    {
        get => PlayerPrefs.GetString("enemysCarpmaDefenderCannon", "");
        set => PlayerPrefs.SetString("enemysCarpmaDefenderCannon", value);
    }
    public string enemysBolmeDefenderCannon
    {
        get => PlayerPrefs.GetString("enemysBolmeDefenderCannon", "");
        set => PlayerPrefs.SetString("enemysBolmeDefenderCannon", value);
    }

    public string playersTrapDeckInfo
    {
        get => PlayerPrefs.GetString("playersTrapDeckInfo", "kapanTrap_0|0#mayinTrap_0|0#");
        set => PlayerPrefs.SetString("playersTrapDeckInfo", value);
    }
    public string enemysTrapDeckInfo
    {
        get => PlayerPrefs.GetString("enemysTrapDeckInfo", "");
        set => PlayerPrefs.SetString("enemysTrapDeckInfo", value);
    }

    public string playersProfileImageSpriteName
    {
        get => PlayerPrefs.GetString("playersProfileImageSpriteName", "");
        set => PlayerPrefs.SetString("playersProfileImageSpriteName", value);
    }
    public string enemysProfileImageSpriteName
    {
        get => PlayerPrefs.GetString("enemysProfileImageSpriteName", "");
        set => PlayerPrefs.SetString("enemysProfileImageSpriteName", value);
    }

    public string playersUserName
    {
        get => PlayerPrefs.GetString("playersUserName", "");
        set => PlayerPrefs.SetString("playersUserName", value);
    }
    public string enemysUserName
    {
        get => PlayerPrefs.GetString("enemysUserName", "");
        set => PlayerPrefs.SetString("enemysUserName", value);
    }

    public int totalToplamaQuestionCount
    {
        get => PlayerPrefs.GetInt("totalToplamaQuestionCount", 1);
        set => PlayerPrefs.SetInt("totalToplamaQuestionCount", value);
    }
    public int totalCikartmaQuestionCount
    {
        get => PlayerPrefs.GetInt("totalCikartmaQuestionCount", 1);
        set => PlayerPrefs.SetInt("totalCikartmaQuestionCount", value);
    }
    public int totalCarpmaQuestionCount
    {
        get => PlayerPrefs.GetInt("totalCarpmaQuestionCount", 1);
        set => PlayerPrefs.SetInt("totalCarpmaQuestionCount", value);
    }
    public int totalBolmeQuestionCount
    {
        get => PlayerPrefs.GetInt("totalBolmeQuestionCount", 1);
        set => PlayerPrefs.SetInt("totalBolmeQuestionCount", value);
    }
    public int correctAnsweredToplamaQuestionCount
    {
        get => PlayerPrefs.GetInt("correctAnsweredToplamaQuestionCount", 0);
        set => PlayerPrefs.SetInt("correctAnsweredToplamaQuestionCount", value);
    }
    public int correctAnsweredCikartmaQuestionCount
    {
        get => PlayerPrefs.GetInt("correctAnsweredCikartmaQuestionCount", 0);
        set => PlayerPrefs.SetInt("correctAnsweredCikartmaQuestionCount", value);
    }
    public int correctAnsweredCarpmaQuestionCount
    {
        get => PlayerPrefs.GetInt("correctAnsweredCarpmaQuestionCount", 0);
        set => PlayerPrefs.SetInt("correctAnsweredCarpmaQuestionCount", value);
    }
    public int correctAnsweredBolmeQuestionCount
    {
        get => PlayerPrefs.GetInt("correctAnsweredBolmeQuestionCount", 0);
        set => PlayerPrefs.SetInt("correctAnsweredBolmeQuestionCount", value);
    }

    public int enemysTotalToplamaQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysTotalToplamaQuestionCount", 1);
        set => PlayerPrefs.SetInt("enemysTotalToplamaQuestionCount", value);
    }
    public int enemysTotalCikartmaQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysTotalCikartmaQuestionCount", 1);
        set => PlayerPrefs.SetInt("enemysTotalCikartmaQuestionCount", value);
    }
    public int enemysTotalCarpmaQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysTotalCarpmaQuestionCount", 1);
        set => PlayerPrefs.SetInt("enemysTotalCarpmaQuestionCount", value);
    }
    public int enemysTotalBolmeQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysTotalBolmeQuestionCount", 1);
        set => PlayerPrefs.SetInt("enemysTotalBolmeQuestionCount", value);
    }
    public int enemysCorrectAnsweredToplamaQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysCorrectAnsweredToplamaQuestionCount", 0);
        set => PlayerPrefs.SetInt("enemysCorrectAnsweredToplamaQuestionCount", value);
    }
    public int enemysCorrectAnsweredCikartmaQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysCorrectAnsweredCikartmaQuestionCount", 0);
        set => PlayerPrefs.SetInt("enemysCorrectAnsweredCikartmaQuestionCount", value);
    }
    public int enemysCorrectAnsweredCarpmaQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysCorrectAnsweredCarpmaQuestionCount", 0);
        set => PlayerPrefs.SetInt("enemysCorrectAnsweredCarpmaQuestionCount", value);
    }
    public int enemysCorrectAnsweredBolmeQuestionCount
    {
        get => PlayerPrefs.GetInt("enemysCorrectAnsweredBolmeQuestionCount", 0);
        set => PlayerPrefs.SetInt("enemysCorrectAnsweredBolmeQuestionCount", value);
    }

    public int lastResultReady
    {
        get => PlayerPrefs.GetInt("lastResultReady", 0);
        set => PlayerPrefs.SetInt("lastResultReady", value);
    }
    public int lastAttackerSideResult
    {
        get => PlayerPrefs.GetInt("lastAttackerSideResult", 0);
        set => PlayerPrefs.SetInt("lastAttackerSideResult", value);
    }
    public int lastDefenderSideResult
    {
        get => PlayerPrefs.GetInt("lastDefenderSideResult", 0);
        set => PlayerPrefs.SetInt("lastDefenderSideResult", value);
    }


    public string LastPlayedStoryID
    {
        get => PlayerPrefs.GetString("LastPlayedStoryID", "");
        set => PlayerPrefs.SetString("LastPlayedStoryID", value);
    }
    public int LastPlayedStoryWinStatus
    {
        get => PlayerPrefs.GetInt("LastPlayedStoryWinStatus", 0);
        set => PlayerPrefs.SetInt("LastPlayedStoryWinStatus", value);
    }
}