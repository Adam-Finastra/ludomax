using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class LudoGameManager : MonoBehaviourPunCallbacks
{
    public enum PlayerColor { Red, Green, Blue, Yellow }

    [Header("Spawn Points")]
    public Transform[] redSpawnPoints;
    public Transform[] greenSpawnPoints;
    public Transform[] blueSpawnPoints;
    public Transform[] yellowSpawnPoints;

    public void Start()
    {
        Debug.Log("Joined room successfully.");
        AssignColorToPlayer(); 
    }

    // Called after any player's properties change
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer.IsLocal && changedProps.ContainsKey("Color"))
        {
            Debug.Log("Local player's color synced: " + changedProps["Color"]);
            SpawnPlayerTokens();
        }
    }

    // Assign color based on ActorNumber (1 to 4 players only)
    void AssignColorToPlayer()
    {
        int actorNum = PhotonNetwork.LocalPlayer.ActorNumber;
        PlayerColor assignedColor = (PlayerColor)((actorNum - 1) % 4);

        Hashtable playerProps = new Hashtable
        {
            { "Color", assignedColor.ToString() }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);
        Debug.Log("Assigned color: " + assignedColor);
    }

    // Spawn 4 tokens based on assigned color
    void SpawnPlayerTokens()
    {
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Color"))
        {
            Debug.LogWarning("No color assigned yet!");
            return;
        }

        string color = PhotonNetwork.LocalPlayer.CustomProperties["Color"].ToString();
        Transform[] spawnPoints = GetSpawnPoints(color);
        string prefabName = color + "Token"; // Must match prefab name in Resources

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            PhotonNetwork.Instantiate(prefabName, spawnPoints[i].position, Quaternion.identity);
        }

        Debug.Log($"<color={color}>Spawned 4 {color} tokens</color>");
    }

    // Get the spawn points array based on color
    Transform[] GetSpawnPoints(string color)
    {
        switch (color)
        {
            case "Red": return redSpawnPoints;
            case "Green": return greenSpawnPoints;
            case "Blue": return blueSpawnPoints;
            case "Yellow": return yellowSpawnPoints;
            default: return new Transform[0];
        }
    }

    // Optional: Debug logs
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player joined: " + newPlayer.NickName);
    }
}
