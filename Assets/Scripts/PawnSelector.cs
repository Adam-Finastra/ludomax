using System.Collections.Generic;
using UnityEngine;

public class PawnSelector : MonoBehaviour
{
    [SerializeField] List<PlayerScript> playerPawns = new List<PlayerScript>();
    private List<PlayerScript> movablePawns = new List<PlayerScript>();

    void Awake()
    {
        PlayerScript[] allTransforms = transform.GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript item in allTransforms)
        {
            if (item != transform)
            {
                playerPawns.Add(item);
            }
        }
    }
    void OnEnable()
    {
        PlayerScript.OnDisableSelection += DisableSelection;
    }
    void OnDisable()
    {
        PlayerScript.OnDisableSelection -= DisableSelection;
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
            foreach (PlayerScript item in movablePawns)
            {
                item.SelectionSwitch(1);
            } 
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
