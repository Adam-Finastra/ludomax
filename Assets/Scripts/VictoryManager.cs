using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] List<string> finishedTeams = new List<string>();
    void OnEnable()
    {
        GameEvent.OnTeamFinished += HandleVictory;
    }
    void OnDisable()
    {
        GameEvent.OnTeamFinished -= HandleVictory;
    }

    public void HandleVictory(string teamName)
    {
        Debug.Log($" victory trigger recived {teamName}");
        finishedTeams.Add(teamName);
    }
}
