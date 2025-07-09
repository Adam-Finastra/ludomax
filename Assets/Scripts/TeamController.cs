using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamController : MonoBehaviour
{
    [SerializeField] GameObject[] TeamUI;
    [SerializeField] int teamIndex;
    private List<Button> buttons = new List<Button>();
    void Awake()
    {
        // buttons = TeamUI.GetComponentsInChildren<Button>().ToList();
    }
}
