using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    [SerializeField] int teamIndex = 0;
    [SerializeField] bool isDebug;
    [SerializeField] private List<TeamScript> teams = new List<TeamScript>();
    private int playerCount;
    private int lastRollValue;

    void Awake()
    {
        teams = GetComponentsInChildren<TeamScript>().
                Where(t => t.GetType() == typeof(TeamScript)).ToList();
    }
    void OnEnable()
    {
        DiceScript.DiceRoll += HandleDiceRoll;
    }
    void Start()
    {
        UIManager.Instance.SaveNextTeam(teamIndex);
        UIManager.Instance.TurnIndication();
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
        PlayerPrefs.SetInt("DiceRoll", value);
        TeamScript team = teams[teamIndex];
        EndTurn();

        switch (value)
        {
            case 6:
                team.HandleSixRoll();
                // Six Roll
                break;
            default:
                team.HandleNormalRoll();
                // Normal Roll
                break;
        }
    }

    private void EndTurn()
    {
        if (lastRollValue != 6)
        {
            teamIndex = (teamIndex + 1) % playerCount;
        }
        UIManager.Instance.SaveNextTeam(teamIndex);
    }

    private void MyLogger(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}
