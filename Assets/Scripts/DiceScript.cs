using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    [SerializeField] GameObject diceAnimation;
    [SerializeField] GameObject diceObjects;
    // [SerializeField] TeamType teamType;
    [SerializeField] bool isLog = true;

    // public int rollValue;
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
        TileScript.OnCancel += ChangeButton;
    }
    void OnDisable()
    {
        TileScript.OnCancel -= ChangeButton;
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

        yield return new WaitForSeconds(1f);

        diceAnimation.SetActive(false);

        if (!DiceBase.IsDebug)
        {
            DiceBase.rollValue = UnityEngine.Random.Range(1, 7);
        }
        Log($"dice rolled :{DiceBase.rollValue}");

        if (DiceBase.rollValue != 6) button.enabled = false; 

        ShowDice(DiceBase.rollValue - 1);
        DiceRoll?.Invoke(DiceBase.rollValue);
    }
    //
    public void ChangeButton()
    {
        Log($" a pawn in request to change the button");
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
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}