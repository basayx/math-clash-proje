using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks
{
    public static PlayerNetworkManager Instance;

    public PhotonView pw;
    public string toplamaCannonID, cikartmaCannonID, carpmaCannonID, bolmeCannonID;
    public string attackerGameDeckInfo;
    public string profileImageSpriteName;
    public string userName;
    public int totalToplamaQuestionCount, totalCikartmaQuestionCount, totalCarpmaQuestionCount, totalBolmeQuestionCount;
    public int correctAnsweredToplamaQuestionCount, correctAnsweredCikartmaQuestionCount, correctAnsweredCarpmaQuestionCount, correctAnsweredBolmeQuestionCount;
    public int level;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        pw = GetComponent<PhotonView>();

        if (pw.ViewID == 1001)
        {
            MainNetworkManager.Instance.player01 = this;
            if (pw.IsMine)
            {
                DataManager.Instance.ThisRoundSide = "attacker";
                pw.RPC("SetDeck", RpcTarget.AllBuffered,
                    DataManager.Instance.playersToplamaDefenderCannon, DataManager.Instance.playersCikartmaDefenderCannon,
                    DataManager.Instance.playersCarpmaDefenderCannon, DataManager.Instance.playersBolmeDefenderCannon,
                    DataManager.Instance.playersAttackerGameDeckInfo,
                    DataManager.Instance.playersProfileImageSpriteName, DataManager.Instance.playersUserName,
                    DataManager.Instance.totalToplamaQuestionCount, DataManager.Instance.totalCikartmaQuestionCount,
                    DataManager.Instance.totalCarpmaQuestionCount, DataManager.Instance.totalBolmeQuestionCount,
                    DataManager.Instance.correctAnsweredToplamaQuestionCount, DataManager.Instance.correctAnsweredCikartmaQuestionCount,
                    DataManager.Instance.correctAnsweredCarpmaQuestionCount, DataManager.Instance.correctAnsweredBolmeQuestionCount,
                    DataManager.Instance.Level
                    );
            }
        }
        else if (pw.ViewID == 2001)
        {
            MainNetworkManager.Instance.player02 = this;
            if (pw.IsMine)
            {
                DataManager.Instance.ThisRoundSide = "defender";
                pw.RPC("SetDeck", RpcTarget.AllBuffered,
                    DataManager.Instance.playersToplamaDefenderCannon, DataManager.Instance.playersCikartmaDefenderCannon,
                    DataManager.Instance.playersCarpmaDefenderCannon, DataManager.Instance.playersBolmeDefenderCannon,
                    DataManager.Instance.playersAttackerGameDeckInfo,
                    DataManager.Instance.playersProfileImageSpriteName, DataManager.Instance.playersUserName,
                    DataManager.Instance.totalToplamaQuestionCount, DataManager.Instance.totalCikartmaQuestionCount,
                    DataManager.Instance.totalCarpmaQuestionCount, DataManager.Instance.totalBolmeQuestionCount,
                    DataManager.Instance.correctAnsweredToplamaQuestionCount, DataManager.Instance.correctAnsweredCikartmaQuestionCount,
                    DataManager.Instance.correctAnsweredCarpmaQuestionCount, DataManager.Instance.correctAnsweredBolmeQuestionCount,
                    DataManager.Instance.Level
                    );
            }
        }

        if (MainNetworkManager.Instance.player01 != null && MainNetworkManager.Instance.player02 != null)
        {
            if (MainNetworkManager.Instance.matchManager != null)
                MainNetworkManager.Instance.matchManager.SetReadyTheRound();
            else
                SceneManager.LoadScene(1);
        }
    }

    [PunRPC]
    public void SetDeck(string toplamaCannonID, string cikartmaCannonID, string carpmaCannonID, string bolmeCannonID, string attackerGameDeckInfo,
        string profileImageSpriteName, string userName,
        int totalToplamaQuestionCount, int totalCikartmaQuestionCount, int totalCarpmaQuestionCount, int totalBolmeQuestionCount,
        int correctAnsweredToplamaQuestionCount, int correctAnsweredCikartmaQuestionCount, int correctAnsweredCarpmaQuestionCount, int correctAnsweredBolmeQuestionCount,
        int level)
    {
        this.toplamaCannonID = toplamaCannonID;
        this.cikartmaCannonID = cikartmaCannonID;
        this.carpmaCannonID = carpmaCannonID;
        this.bolmeCannonID = bolmeCannonID;
        this.attackerGameDeckInfo = attackerGameDeckInfo;
        this.profileImageSpriteName = profileImageSpriteName;
        this.userName = userName;
        this.totalToplamaQuestionCount = totalToplamaQuestionCount;
        this.totalCikartmaQuestionCount = totalCikartmaQuestionCount;
        this.totalCarpmaQuestionCount = totalCarpmaQuestionCount;
        this.totalBolmeQuestionCount = totalBolmeQuestionCount;
        this.correctAnsweredToplamaQuestionCount = correctAnsweredToplamaQuestionCount;
        this.correctAnsweredCikartmaQuestionCount = correctAnsweredCikartmaQuestionCount;
        this.correctAnsweredCarpmaQuestionCount = correctAnsweredCarpmaQuestionCount;
        this.correctAnsweredBolmeQuestionCount = correctAnsweredBolmeQuestionCount;
        this.level = level;
    }

    [PunRPC]
    public void ATrapPlacemented(string trapAccessName, Vector3 pos)
    {
        MainNetworkManager.Instance.matchManager.enemyDefenderManager.ATrapPlacemented(trapAccessName, pos);
    }

    [PunRPC]
    public void DefenderPreparationCompleted()
    {
        MainNetworkManager.Instance.matchManager.PreparationCompleted();
    }

    [PunRPC]
    public void AnAttackerCharacterSpawned(string characterID, int roadNo)
    {
        MainNetworkManager.Instance.matchManager.enemyAttackerManager.AnAttackerCharacterSpawned(characterID, roadNo);
    }

    [PunRPC]
    public void EnemyEscaped()
    {
        MainNetworkManager.Instance.matchManager.EnemyEscaped();
    }
}
