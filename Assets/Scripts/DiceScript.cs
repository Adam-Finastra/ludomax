using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    [SerializeField] GameObject diceAnimation;
    [SerializeField] GameObject diceObjects;
    [SerializeField] bool isLog = false;
    [SerializeField] bool isDebug = true;
    [SerializeField] int rollValue = 2;

    private int lastValue;
    private Image[] diceSprites;
    public static Action<int> DiceRoll;

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
    }

    private IEnumerator RollDice()
    {
        diceAnimation.SetActive(true);

        yield return new WaitForSeconds(1f);

        diceAnimation.SetActive(false);

        if (!isDebug)
        {
            rollValue = UnityEngine.Random.Range(1, 7);
        }
        Log($"dice rolled :{rollValue}");
        ShowDice(rollValue - 1);
        DiceRoll?.Invoke(rollValue);
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
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}