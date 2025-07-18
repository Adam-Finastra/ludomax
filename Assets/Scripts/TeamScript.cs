using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TeamType
{
    Blue,
    Red,
    Green,
    Yellow
}
public class TeamScript : MonoBehaviour
{
    public TeamType teamType;
    public int startIndex;
    public bool isDebug;

     public List<PlayerScript> playerPawns = new List<PlayerScript>();
     public List<PlayerScript> movablePawns = new List<PlayerScript>();

    private PawnSelector pawnSelector;

    void Awake()
    {
        pawnSelector = GetComponent<PawnSelector>();
        playerPawns = GetComponentsInChildren<PlayerScript>().ToList(); // Assign here
    }

    public void HandleSixRoll()
    {
        MyLogger("Hey six roll func is working");
        pawnSelector.EnableSelection();
    }

    public void HandleNormalRoll()
    {
        MyLogger("Hey normal roll func is working");
        pawnSelector.OnlyEnableMovable();
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
