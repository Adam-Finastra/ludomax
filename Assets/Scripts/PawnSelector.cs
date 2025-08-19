using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class PawnSelector : MonoBehaviourPun
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
        if (photonView.IsMine)
        {
            foreach (PlayerScript item in team.playerPawns)
            {
                item.SelectionSwitch(1);
            }
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

        if (lastadded != movablePawn)
        {
            team.movablePawns.Add(team.playerPawns[movablePawn]);
            lastadded = movablePawn;
        }
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
