using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnSelector : MonoBehaviour
{
    private TeamScript team;
    [SerializeField] bool isDebug;

    private int lastadded = -1;

    void Awake()
    {
        team = GetComponent<TeamScript>();
    }

    public void EnableSelection()
    {
        foreach (PlayerScript item in team.playerPawns)
        {
            item.SelectionSwitch(1);
        }
    }

    public void OnlyEnableMovable()
    {
        if (team.movablePawns.Count > 0)
        {
            MyLogger($"Movable token count: {team.movablePawns.Count}");
            foreach (PlayerScript item in team.movablePawns)
            {
                if (!item.inJail)
                {
                    item.SelectionSwitch(1);
                }
                else
                {
                    MyLogger("Player is in jail.");
                }
            }
        }
        else
        {
            MyLogger("No movable tokens.");
            UIManager.Instance.TurnIndication();
        }
    }

    public void DisableSelection(int movablePawn)
    {
        foreach (PlayerScript item in team.playerPawns)
        {
            item.SelectionSwitch(0);
        }

        if (!SearchList(team.movablePawns, team.playerPawns[movablePawn]))
        {
            MyLogger($" added {team.playerPawns[movablePawn].name} to movable list");
            team.movablePawns.Add(team.playerPawns[movablePawn]);
            lastadded = movablePawn;
        }
        else
        {
            MyLogger($" tried to add pawn to movable list : ALREADY EXISTS");
        }
    }
    private bool SearchList(List<PlayerScript> list, PlayerScript player)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == player)
            {
                return true;
            }
        }
        return false;
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
