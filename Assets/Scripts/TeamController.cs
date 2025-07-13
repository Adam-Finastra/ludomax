using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    [SerializeField] int playerCount;
    [SerializeField] int teamIndex = 0;
    [SerializeField] bool isDebug;

    private int lastRollValue;

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
        Logger($" dice rolled : value is  {value}");
        lastRollValue = value;
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

    private void Logger(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}
