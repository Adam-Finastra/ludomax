using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    [SerializeField] List<GameObject> Players = new List<GameObject>();
    // [SerializeField] int playerCount;
    [SerializeField] GameObject[] TeamUI;
    [SerializeField] int teamIndex = 0;
    [SerializeField] bool isDebug;

    private int lastRollValue;

    void Start()
    {
        ManagePlayers();
        TurnIndication();
    }
    void OnEnable()
    {
        DiceScript.DiceRoll += HandleDiceRoll;
    }
    void OnDisable()
    {
        DiceScript.DiceRoll -= HandleDiceRoll;
    }

    public void ManagePlayers()
    {
        // for (int i = 0; i < Players.Count; i++)
        // {
        //     if (Players != null)
        //     {
        //         Players[i].SetActive(i < playerCount);
        //     }
        // }
    }
    public void TurnIndication()
    {
        for (int i = 0; i < TeamUI.Length; i++)
        {
            TeamUI[i].SetActive(i == teamIndex);
        }
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
            teamIndex = (teamIndex + 1) % Players.Count;
        }
        TurnIndication();
    }

    private void Logger(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}
