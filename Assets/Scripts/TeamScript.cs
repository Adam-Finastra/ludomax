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
    [SerializeField] TeamType teamType;
    [SerializeField] int startPosition;
    [SerializeField] bool isDebug;

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
    }

    private void MyLogger(string message)
    {
        if (isDebug) Debug.Log(message);
    }
}
