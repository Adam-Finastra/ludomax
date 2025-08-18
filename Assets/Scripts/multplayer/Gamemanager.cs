using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using System;

public class Gamemanager : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public TMP_InputField nickname;
    public static Gamemanager instance;

    [Header("Settings")]
    public string targetSceneName = "GameScene"; // Use scene name instead of index
    public int maxPlayersPerRoom = 2;

    private string playername;
    private string roomname;
    private bool isConnecting = false;
    string lastroomname;
    int playersInLobby;
    bool isJoiningRoom = false;
    string lastnameroom;
    private void Awake()
    {
        

        PhotonNetwork.AutomaticallySyncScene = true;

        // Connect to Photon
        ConnectToPhoton();
       
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    private void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected && !isConnecting)
        {
            isConnecting = true;
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
       
        Debug.Log("Connected to Photon Master Server");
        isConnecting = false;
       
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby successfully");
       
    }
    

    /*
    public override void OnDisconnected(DisconnectCause cause)
    {
        
        isConnecting = false;

        // Attempt to reconnect
        if (cause != DisconnectCause.DisconnectByClientLogic)
        {
            Invoke(nameof(ConnectToPhoton), 2f);
        }
    }
    */

    public void JoinOrCreateRoom()
    {
        
        // Validate input
        if (nickname == null)
        {
          
            return;
        }

        playername = nickname.text.Trim();
       

        if (string.IsNullOrEmpty(playername))
        {
           
            return;
        }

      
        

       // Set player nickname
        PhotonNetwork.NickName = playername;
        
        Debug.Log($"Attempting to join/create room with nickname: {PhotonNetwork.NickName}");

       PhotonNetwork.JoinRandomRoom();



    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"No random room available: {message}. Creating new room...");
        CreateNewRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join room: {message} (Code: {returnCode})");
        CreateNewRoom();
    }

    private void CreateNewRoom()
    {
        roomname = $"Room_{UnityEngine.Random.Range(1000, 9999)}_{System.DateTime.Now.Ticks % 1000}";

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayersPerRoom,
            IsVisible = true,
            IsOpen = true
        };

        Debug.Log($"Creating room: {roomname}");
        PhotonNetwork.CreateRoom(roomname, roomOptions, TypedLobby.Default);
         
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

        CreateNewRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Successfully joined room: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"Players in room: {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}");

        // Load the game scene
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Loading game scene...");
            PhotonNetwork.LoadLevel(1);
        }
    }
    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} joined the room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} left the room");
    }

  

    private void OnApplicationPause(bool pauseStatus)
    {
        // Handle app pause/resume for mobile
        if (pauseStatus && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.SendAllOutgoingCommands();
        }
    }
    public void LeftRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom(); // Don't load scene here
            Debug.Log(" trying laving the room");
        }
        else
        {
            LoadMainMenuScene();
        }
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Successfully left the room.");
        LoadMainMenuScene();
    }

    void LoadMainMenuScene()
    {
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.ConnectUsingSettings();
    }

}