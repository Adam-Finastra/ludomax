using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceBase : MonoBehaviour
{
    [SerializeField] private bool isDebugProxy;
    [SerializeField] private int rollValueProxy;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_InputField inputField;

    public static int rollValue { get; set; }
    public static bool IsDebug { get; private set; }

    void Start()
    {
        toggle.onValueChanged.AddListener(ChangeBool);
        inputField.onValueChanged.AddListener(ChangeValue);

        isDebugProxy = toggle.isOn;
    }
    void Update()
    {
        if (isDebugProxy != IsDebug) IsDebug = isDebugProxy;

        if (rollValueProxy != rollValue) rollValue = rollValueProxy;
    }
    public void ChangeBool(bool turn)
    {
        isDebugProxy = turn;
    }
    public void ChangeValue(string number)
    {
        rollValueProxy = int.Parse(number);
    }
}
