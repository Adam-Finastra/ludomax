using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    [SerializeField] GameObject diceAnimation;
    [SerializeField] GameObject diceObjects;
    [SerializeField] bool isDebug = true;
    private List<int> weightList = new List<int>();
    private int rolledTimes = 0;
    private int lastValue;
    private Image[] diceSprites;
    private Button button;
    public static Action<int> DiceRoll;
    
    void Awake()
    {
        button = GetComponent<Button>();
    }
    void OnEnable()
    {
        button.enabled = true;
        GameEvent.EnableButton += ChangeButton;
    }
    void OnDisable()
    {
        GameEvent.EnableButton -= ChangeButton;
    }
    void Start()
    {
        diceAnimation.SetActive(false);
        diceSprites = diceObjects.GetComponentsInChildren<Image>();

        foreach (var item in diceSprites)
        {
            item.enabled = false;
        }
    }

    public void StartDice()
    {
        Log($" animation started!");
        StartCoroutine(RollDice());
        // button.enabled = false;
    }

    private IEnumerator RollDice()
    {
        diceAnimation.SetActive(true);
        // sfx.sfxinstance.dicesfx();

        yield return new WaitForSeconds(0.5f);

        diceAnimation.SetActive(false);

        if (!DiceBase.IsDebug)
        {
            DiceBase.rollValue = UnityEngine.Random.Range(1, 7);
            lastValue = DiceBase.rollValue;
        }

        button.enabled = false;
        ShowDice(DiceBase.rollValue - 1);
        DiceRoll?.Invoke(DiceBase.rollValue);
        Log($"dice rolled :{DiceBase.rollValue} & probablity tracker is {rolledTimes}");
    }
    private int StartRoll(int indexss = 0)
    {
        int[] weight = { 1, 1, 1, 1, 1, indexss };
        weightList.Clear();
        for (int i = 0; i < weight.Length; i++)
        {
            for (int j = 0; j < weight[i]; j++)
            {
                weightList.Add(i + 1);
            }
        }
        int randointex = UnityEngine.Random.Range(0, weightList.Count);
        int rollednumber = weightList[randointex];
        // Debug.Log(rollednumber);
        return rollednumber;
    }
    public void ChangeButton()
    {
        Log($" a pawn in request to change the button");
        // button.enabled = button.enabled == true ? false : true;
        button.enabled = true;
    }
    private void ShowDice(int value)
    {
        for (int i = 0; i < diceSprites.Length; i++)
        {
            diceSprites[i].enabled = (i == value);
        }
    }
    private void Log(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}