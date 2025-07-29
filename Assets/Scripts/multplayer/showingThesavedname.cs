using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;


public class showingThesavedname : MonoBehaviourPunCallbacks
{
    public static showingThesavedname instance;
    [SerializeField] private TextMeshProUGUI[] playernames;

   [SerializeField] private GameObject[] playerindicator;

    int index;



    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;

        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        indicator();
        playercound();
    }
    private void Start()
    {
        updateplayernames();
        indicator();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updateplayernames();
        indicator();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updateplayernames();
        indicator();
    }

    void updateplayernames()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < playernames.Length; i++)
        {
            if (i < players.Length)
            {
                playernames[i].text = players[i].NickName;
            }
           
        }
    }
    void indicator()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < playernames.Length; i++)
        {
            if (i < players.Length && players[i] == PhotonNetwork.MasterClient)
            {
                playerindicator[i].SetActive(true);
            }
            else
            {
                playerindicator[i].SetActive(false);
            }
        }


    }
    public void transferownership()
    {
        Player[] player = PhotonNetwork.PlayerList;
        
        if(player.Length < 1)
        {
            Debug.Log("no player is found ");
                return;
        }
        else
        {
            int currentindx = -1;
            for(int i  = 0; i < player.Length; i++)
            {
                if (player[i].IsMasterClient)
                {
                   currentindx = i;
                    break;
                }
            }
            int nextindex;
            nextindex = (currentindx+ 1 ) % player.Length;

           Player targetedplayer = player[nextindex];
            PhotonNetwork.SetMasterClient(targetedplayer);

        }

    }
    public void ownershipchangeusingactor(int actorNumber)
    {
        Player targetPlayer = PhotonNetwork.CurrentRoom.GetPlayer(actorNumber);
        if (targetPlayer != null && targetPlayer != PhotonNetwork.MasterClient)
        {
            PhotonNetwork.SetMasterClient(targetPlayer);
            Debug.Log("Transferring master client to: " + targetPlayer.NickName);
        }
        else
        {
            Debug.LogWarning("Target player not found or is already master client");
        }
    }

    public void LeftRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            StartCoroutine(loadlevel());
        }
    }
    IEnumerator loadlevel()
    {
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel(0);
    }
    public void Diconnectroom()
    {
       if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
           
        }
    }
   void playercound()
    {
        int index;
        Player[] player = PhotonNetwork.PlayerList;
        index = player.Length;
        Debug.Log("playercound is" + index);   
    }
    public string lastroomjoinname()
    {
        string lastroomname = PhotonNetwork.CurrentRoom.Name;
       return lastroomname;
        
    }

  

    /*
    public void MakeNextPlayerMaster()
    {
        Player[] players = PhotonNetwork.PlayerList;

       
      
        {
            if (getmasterindex(players) == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                int currentindex = getmasterindex(players);
                int nextIndex = (currentindex + 1) % players.Length;
                Player targetPlayer = players[nextIndex];

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.SetMasterClient(targetPlayer);
                    Debug.Log("Transferred master client to: " + targetPlayer.NickName);
                }
                else
                {
                    Debug.LogWarning("Only current Master Client can transfer master role.");
                }

               
            }
        }
    }
    int getmasterindex(Player[] players)
    {
        int index = 0;
        foreach(Player p in players) {

            if (p.IsMasterClient)
            {
                index = p.ActorNumber;
            }
           
        
        }
        return index;
    }
*/

}
