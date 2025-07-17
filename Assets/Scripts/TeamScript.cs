using UnityEngine;

public enum TeamType
{
    Blue,
    Red,
    Green,
    Yellow
}
public class TeamScript : MonoBehaviour
{
    public TeamType teamType;
    public int startPosition;
    public bool isDebug;
    private PawnSelector pawnSelector;

    void Awake()
    {
        pawnSelector = GetComponent<PawnSelector>();
    }

    public void HandleSixRoll()
    {
        MyLogger($" hey six roll func is working ");
        pawnSelector.EnableSelection();
    }
    public void HandleNormalRoll()
    {
        MyLogger($" hey normal roll func is working ");
        pawnSelector.OnlyEnableMovable();
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
