using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Players = new List<GameObject>();

    [SerializeField] int playerCount;

    void Start()
    {
        ManagePlayers();
    }
    public void ManagePlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players != null)
            {
                Players[i].SetActive(i < playerCount);
            }
        }
    }
}
