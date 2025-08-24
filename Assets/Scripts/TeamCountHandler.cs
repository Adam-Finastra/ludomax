using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TeamPair
{
    public GameObject Dice;
    public GameObject UI;

    public void ChangeElementState(bool state)
    {
        Dice.SetActive(state);
        UI.SetActive(state);
    }
}
public class TeamCountHandler : MonoBehaviour
{
    public TeamPair[] gbPair;
    private int playerCount;

    void Awake()
    {
        playerCount = PlayerPrefs.GetInt("NumberOfPlayers");
        Debug.Log($" player Count is {playerCount}");

        HandlePlayerCount(playerCount);
    }

    private void DisableTeams(int count)
    {
        for (int i = 0; i < gbPair.Length; i++)
        {
            if (i >= count)
            {
                gbPair[i].ChangeElementState(false);
            }
        }
    }
    private void HandlePlayerCount(int count)
    {
        switch (count)
        {
            case 2:
                HandleTwoPlayer();
                break;
            default:
                DisableTeams(count);
                break;
        }
    }
    private void HandleTwoPlayer()
    {
        gbPair[1].ChangeElementState(false);
        gbPair[3].ChangeElementState(false);
    }
}
