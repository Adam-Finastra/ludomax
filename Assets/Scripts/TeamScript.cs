using System.Collections.Generic;
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
    [SerializeField] TeamType teamType;
    [SerializeField] int startPosition;
    [SerializeField] bool isDebug;

    private TurnHandler turnHandler;
    private List<Transform> players = new List<Transform>();

    void Awake()
    {
        Transform[] allTransforms = transform.GetComponentsInChildren<Transform>();
        foreach (Transform item in allTransforms)
        {
            if (item != transform)
            {
                players.Add(item);
            }
        }

        turnHandler = GetComponent<TurnHandler>();
    }
    void OnEnable()
    {
        DiceScript.DiceRoll += HandleInput;
    }
    void OnDisable()
    {
        DiceScript.DiceRoll -= HandleInput;
    }
    public void HandleInput(int rollValue)
    {
        Logger($" hey its working ");
        // turnHandler.ProcessTurn
    }

    private void Logger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
