using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnSelector : MonoBehaviour
{
    [SerializeField] List<PlayerScript> playerPawns = new List<PlayerScript>();
    [SerializeField] private List<PlayerScript> movablePawns = new List<PlayerScript>();

    void Awake()
    {
        playerPawns = GetComponentsInChildren<PlayerScript>().ToList();
    }

    public void EnableSelection()
    {
        foreach (PlayerScript item in playerPawns)
        {
            item.SelectionSwitch(1);
        }
    }
    public void OnlyEnableMovable()
    {
        if (movablePawns.Count > 0)
        {
            Debug.Log($" movable token is not zero, rather count is {movablePawns.Count}");
            foreach (PlayerScript item in movablePawns)
            {
                if (!item.inJail)
                {
                    item.SelectionSwitch(1);
                }
            }
        }
        else
        {
            Debug.Log($" movable token count is {movablePawns.Count}");
            // UIManager.Instance.SaveNextTeam()
            UIManager.Instance.TurnIndication();
        }
    }
    public void DisableSelection(int movablePawn)
    {
        foreach (PlayerScript item in playerPawns)
        {
            item.SelectionSwitch(0);
        }

        movablePawns.Add(playerPawns[movablePawn]);
    }
}
