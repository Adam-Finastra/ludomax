using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool isLog;
    private List<PlayerScript> allpawns = new List<PlayerScript>();
    public enum TileType
    {
        Normal,
        SafeZone
    }
    public TileType tileType;

    public void OnPlayerLands(PlayerScript arrivingPlayer)
    {
        Log($"{arrivingPlayer.name} of {arrivingPlayer.teamType} arrived on {this.name}");
        
        switch (tileType)
        {
            case TileType.Normal:
                HandlePawnCancel(arrivingPlayer);
                break;
            case TileType.SafeZone:
                HandleSafeZone(arrivingPlayer);
                break;
        }
    }

    private void HandlePawnCancel(PlayerScript arrivingPlayer)
    {
        Log($" ready to cancel on {this.name}");
        allpawns.Add(arrivingPlayer);
        List<PlayerScript> toCancel = new List<PlayerScript>();
        foreach (var otherPawn in allpawns)
        {
            if (otherPawn == arrivingPlayer) continue;
            if (otherPawn.teamType == arrivingPlayer.teamType) continue;
            if (otherPawn.playerPosition == arrivingPlayer.playerPosition)
            {
                toCancel.Add(otherPawn);
            }
        }

        if (toCancel.Count > 0)
        {
            foreach (var item in toCancel)
            {
                // CancelPawn(item);
            }
        }
        else
        {
          UIManager.Instance.TurnIndication(); 
        }
    }
    private void HandleSafeZone(PlayerScript player)
    {
        Log($" Reached safe zone");
        UIManager.Instance.TurnIndication();
    }
    // private void CancelPawn(PlayerScript pawn)
    // {
    //     pawn.inJail = true;
    //     pawn.playerPosition = -1;
    //     pawn.transform.position = pawn.startPoint.position;
    //     allpawns.Remove(pawn);

    //     TeamScript team = pawn.GetComponentInParent<TeamScript>();
    //     team.moveablePawns.Remove(pawn);
    //     team.pawns.Add(pawn);
    //     // PlayerController playerController = pawn.GetComponentInParent<PlayerController>();
    //     // playerController.GiveChance();
    //     UIManager.Instance.GiveChance();
    //     Log($"{pawn.name} was cancelled!");
    // }

    private void Log(string message)
    {
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}