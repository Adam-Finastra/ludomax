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
    private int lastTeamIndex;

    void Awake()
    {
        teams = GetComponentsInChildren<TeamScript>().
                Where(t => t.GetType() == typeof(TeamScript)).ToList();

        playerCount = transform.childCount;
        PlayerPrefs.SetInt("TeamCount", playerCount);
        PlayerPrefs.Save();    }
    void OnEnable()
    {
        DiceScript.DiceRoll += HandleDiceRoll;
    }
    void Start()
    {
        UIManager.Instance.SaveNextTeam(teamIndex);
        UIManager.Instance.TurnIndication();

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
    public void GiveChance()
    {
        teamIndex = lastTeamIndex;
        UIManager.Instance.SaveNextTeam(teamIndex);
    }
    private void EndTurn()
    {
        lastTeamIndex = teamIndex;
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
