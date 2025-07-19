using System;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool isLog;
    private List<PlayerScript> allpawns = new List<PlayerScript>();
    public static Action OnCancel;
    public enum TileType
    {
        Normal,
        SafeZone,
        FinishLine
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
            case TileType.FinishLine:
                HandleWin(arrivingPlayer);
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
                CancelPawn(item);
            }
        }
        else
        {
          UIManager.Instance.TurnIndication();  
        }
    }
    private void HandleWin(PlayerScript player)
    {
        Log($" {player.name} reached finish line");
        RemovePlayerFromGame(player);
    }
    private void HandleSafeZone(PlayerScript player)
    {
        Log($" Reached safe zone");
        UIManager.Instance.TurnIndication();
    }
    private void CancelPawn(PlayerScript pawn)
    {
        pawn.inJail = true;
        pawn.playerPosition = -1;
        pawn.transform.position = pawn.startPos.position;
        allpawns.Remove(pawn);

        RemovePlayerFromGame(pawn);

        Log($"{pawn.name} was cancelled!");
    }
    private void RemovePlayerFromGame(PlayerScript player)
    {
        TeamScript team = player.GetComponentInParent<TeamScript>();
        team.movablePawns.Remove(player); // removes chanced player from movable list
        // team.playerPawns.Add(pawn);
        OnCancel?.Invoke();

        TeamController teamController = team.GetComponentInParent<TeamController>();
        teamController.GiveChance(); // rewards a chance after cancelling
        UIManager.Instance.TurnIndication();  // showing UI indication
    }
    private void Log(string message)
    {
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}