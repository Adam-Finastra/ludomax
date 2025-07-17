using UnityEngine;

public class DiceBase : MonoBehaviour
{
    [SerializeField] private bool isDebugProxy;
    [SerializeField] private int rollValueProxy;

    public static int rollValue { get; set; }
    public static bool IsDebug { get; private set;  }

    void Update()
    {
        if (isDebugProxy != IsDebug) IsDebug = isDebugProxy;

        if (rollValueProxy != rollValue) rollValue = rollValueProxy;
    }
}
