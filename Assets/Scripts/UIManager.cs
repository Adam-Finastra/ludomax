using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject[] TeamUI;
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
    public void SaveNextTeam(int teamIndex)
    {
        nextTeamIndex = teamIndex;
        MyLogger($" saved next turn as {nextTeamIndex} ");
    }
    public void TurnIndication()
    {
        for (int i = 0; i < TeamUI.Length; i++)
        {
            TeamUI[i].SetActive(i == nextTeamIndex);
        }
        MyLogger($" turn changed to {nextTeamIndex} ");
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
