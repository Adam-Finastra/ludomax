using UnityEngine;

public class TeamController : MonoBehaviour
{
    [SerializeField] GameObject[] TeamUI;
    [SerializeField] int teamIndex = 0;
    [SerializeField] bool isDebug;

    private int lastRollValue;

    void OnEnable()
    {
        DiceScript.DiceRoll += HandleDiceRoll;
    }
    void OnDisable()
    {
        DiceScript.DiceRoll -= HandleDiceRoll;
    }
    void Start()
    {
        TurnIndication();
    }

    public void TurnIndication()
    {
        for (int i = 0; i < TeamUI.Length; i++)
        {
            // if (i == teamIndex)
            // {
            //     TeamUI[i].SetActive(true);
            //     Logger($" team index {i} is turned off");
            // }
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
            teamIndex = (teamIndex + 1) % 4;
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
