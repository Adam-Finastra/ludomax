using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject[] TeamUI;
    [SerializeField] List<GameObject> teamUI = new List<GameObject>();
    [SerializeField] bool isDebug;

    private int nextTeamIndex;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < TeamUI.Length; i++)
        {
            if (TeamUI[i].activeInHierarchy)
            {
                teamUI.Add(TeamUI[i]);
            }
        }
    }
    public void SaveNextTeam(int teamIndex)
    {
        nextTeamIndex = teamIndex;
        MyLogger($" saved next turn as {nextTeamIndex} ");
    }
    public void TurnIndication()
    {
        for (int i = 0; i < teamUI.Count; i++)
        {
            teamUI[i].SetActive(i == nextTeamIndex);
        }
        MyLogger($" turn changed to {nextTeamIndex} ");
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
