using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool inJail = true;
    public Transform startPos;
    [SerializeField] int playerIndex;
    public TeamType teamType;
    [SerializeField] int startIndex;
    [SerializeField] bool isDebug;
    public int playerPosition = 0;
    [SerializeField] private ParticleSystem selectionIndication;
    private PawnSelector _pawnSelector;
    private PlayerMovement playerMovement;
    private List<Transform> commonTiles = new List<Transform>();
    private int teamCount;
    private int targetPosition;
    public int steps = 0;
    public bool isSelectable;

    void Awake()
    {
        _pawnSelector = GetComponentInParent<PawnSelector>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start()
    {
        commonTiles = TileManager.Instance.CommonTiles();
        playerIndex = int.Parse(this.gameObject.name);
        teamCount = PlayerPrefs.GetInt("TeamCount", 0);
    }
    void OnMouseDown()
    {
        MyLogger($" {steps}");
        if (!isSelectable) return;

        targetPosition = PlayerPrefs.GetInt("DiceRoll", 0);

        if (inJail)
        {
            playerMovement.JumpToPosition(commonTiles[startIndex].position);
            inJail = false;
        }
        else
        {
            // StartCoroutine(StartJump(GetIndex()));
            playerMovement.IntialJumpLoop(this, GetIndex());
        }

        _pawnSelector.DisableSelection(playerIndex - 1);
        selectionIndication.Stop();
    }

    // Finding Index of the common tile
    private int GetIndex()
    {
        return (steps + targetPosition) % commonTiles.Count;
    }
    // Turn selection bool off and on
    public void SelectionSwitch(int value)
    {
        isSelectable = value > 0 ? true : false;
        TokenUI();
    }

    private void TokenUI()
    {
        if (isSelectable)
        {
            selectionIndication.Play();
        }
        else
        {
            selectionIndication.Stop();
        }
    }

    private void MyLogger(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}
