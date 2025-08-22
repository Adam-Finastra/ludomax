using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool isLog;
    [SerializeField] private List<PlayerScript> allpawns = new List<PlayerScript>();
    // private bool wasCancelled = false;
    // public static Action OnCancel;
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

        // Searching for players of other team on the tile.
        foreach (var otherPawn in allpawns)
        {
            if (otherPawn == arrivingPlayer) continue;
            if (otherPawn.teamType == arrivingPlayer.teamType) continue;
            if (otherPawn.playerPosition == arrivingPlayer.playerPosition)
            {
                CancelPawn(otherPawn); // immediately cancelling pawn found.
                return;
            }
        }

        UIManager.Instance.TurnIndication();
        GameEvent.EnableButton?.Invoke();
    }
    private void HandleWin(PlayerScript player)
    {
        Log($" {player.name} reached finish line");
        RemovePlayerFromGame(player,true);
    }
    private void HandleSafeZone(PlayerScript player)
    {
        Log($" Reached safe zone");
        UIManager.Instance.TurnIndication();
        GameEvent.EnableButton?.Invoke();
    }
    private void CancelPawn(PlayerScript pawn)
    {
        pawn.inJail = true;
        pawn.playerPosition = pawn.startIndex;
        pawn.steps = 0;
        pawn.transform.position = pawn.startPos.position;
        allpawns.Remove(pawn);

        RemovePlayerFromGame(pawn,false);

        Log($"{pawn.name} was cancelled!");
    }
    private void RemovePlayerFromGame(PlayerScript player, bool state)
    {
        TeamScript team = player.GetComponentInParent<TeamScript>();
        team.movablePawns.Remove(player); // removes cancelled player from movable list
        if (state)
        {
            team.playerPawns.Remove(player); // removes player from the game.
        }
        GameEvent.EnableButton?.Invoke();

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