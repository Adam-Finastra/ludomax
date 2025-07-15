using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    [SerializeField] int teamIndex = 0;
    [SerializeField] bool isDebug;
    private List<TeamScript> teams = new List<TeamScript>();
    private int playerCount;
    private int lastRollValue;

    void Awake()
    {
        TeamScript[] allTransforms = transform.GetComponentsInChildren<TeamScript>();
        foreach (TeamScript item in allTransforms)
        {
            if (item != transform)
            {
                teams.Add(item);
            }
        }
    }
    void OnEnable()
    {
        DiceScript.DiceRoll += HandleDiceRoll;
    }
    void Start()
    {
        UIManager.Instance.TurnIndication(teamIndex);
        playerCount = transform.childCount;
    }
    void OnDisable()
    {
        DiceScript.DiceRoll -= HandleDiceRoll;
    }

    public void HandleDiceRoll(int value)
    {
        MyLogger($" dice rolled : value is  {value}");
        lastRollValue = value;

        switch (value)
        {
            case 6:
                teams[teamIndex].HandleSixRoll();
                PlayerPrefs.SetInt("DiceRoll", value);
                // Six Roll
                break;
            default:
                teams[teamIndex].HandleNormalRoll();
                // Normal Roll
                break;
        }

        EndTurn();
    }

    private void EndTurn()
    {
        if (lastRollValue != 6)
        {
            teamIndex = (teamIndex + 1) % playerCount;
        }
        UIManager.Instance.TurnIndication(teamIndex);
    }

    private void MyLogger(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}
