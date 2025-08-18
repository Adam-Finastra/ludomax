using UnityEngine;
using UnityEngine.UI;

public class DiceBase : MonoBehaviour
{
    [SerializeField] private bool isDebugProxy;
    [SerializeField] private int rollValueProxy;
  //  [SerializeField] private Toggle toggle;

    public static int rollValue { get; set; }
    public static bool IsDebug { get; private set; }

    void Start()
    {
        //toggle.onValueChanged.AddListener(ChangeBool);

       // isDebugProxy = toggle.isOn;
    }
    void Update()
    {
        if (isDebugProxy != IsDebug) IsDebug = isDebugProxy;

        if (rollValueProxy != rollValue) rollValue = rollValueProxy;
    }
    /*
    public void ChangeBool(bool turn)
    {
        isDebugProxy = turn;
    }
    */
}
