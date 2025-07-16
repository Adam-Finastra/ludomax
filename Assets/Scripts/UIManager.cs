using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject[] TeamUI;

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
        Debug.Log($" saved next turn as {nextTeamIndex} ");
    }
    public void TurnIndication()
    {
        for (int i = 0; i < TeamUI.Length; i++)
        {
            TeamUI[i].SetActive(i == nextTeamIndex);
        }
        Debug.Log($" turn changed to {nextTeamIndex} ");
    }

}
