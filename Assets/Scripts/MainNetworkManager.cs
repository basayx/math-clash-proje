using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MainNetworkManager : MonoBehaviourPunCallbacks
{
    public static MainNetworkManager Instance;
    public MenuManager menuManager;
    public MatchManager matchManager;
    public PlayerNetworkManager player01;
    public PlayerNetworkManager player02;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (menuManager != null)
            menuManager.karsilasmaAraButton.interactable = false;

        PhotonNetwork.ConnectUsingSettings();
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinOrCreateRoom("OdaIsmi", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        //PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Servere Girildi");

        PhotonNetwork.JoinLobby();

        base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye Girildi");
        menuManager.karsilasmaAraButton.interactable = true;

        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya Girildi");
        PlayerNetworkManager playerNet = PhotonNetwork.Instantiate("PlayerNetwork", Vector3.zero, Quaternion.identity, 0, null).gameObject.GetComponent<PlayerNetworkManager>();
        playerNet.pw = playerNet.gameObject.GetComponent<PhotonView>();

        base.OnJoinedRoom();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden Çıkıldı");
        menuManager.karsilasmaAraButton.interactable = false;

        base.OnLeftLobby();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (matchManager != null && matchManager.roundStatus == MatchManager.RoundStatuses.started)
        {
            if (player01.pw.IsMine)
                player02.pw.RPC("EnemyEscaped", RpcTarget.All, null);
            else if (player02.pw.IsMine)
                player01.pw.RPC("EnemyEscaped", RpcTarget.All, null);
        }

        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Odadan Çıkıldı");

        base.OnLeftRoom();
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Hata: Odaya girilemedi.");

        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("Hata: Herhangi bir oynaya girilemedi.");

        base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Hata: Oda oluşturulamadı.");

        base.OnCreateRoomFailed(returnCode, message);
    }
}
