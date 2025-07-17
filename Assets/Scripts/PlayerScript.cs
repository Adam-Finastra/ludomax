using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerScript : TeamScript
{
    public bool inJail = true;
    [SerializeField] int playerIndex;
    public int playerPosition = 0;
    // [SerializeField] int startPosition;
    [SerializeField] float jumpHeight = 0.1f;
    // [SerializeField] bool isDebug;
    [SerializeField] private ParticleSystem selectionIndication;
    private PawnSelector _pawnSelector;
    private List<Transform> commonTiles = new List<Transform>();
    private int targetPosition;
    private int steps;
    private bool isSelectable;

    void Awake()
    {
        _pawnSelector = GetComponentInParent<PawnSelector>();
    }
    void Start()
    {
        commonTiles = TileManager.Instance.CommonTiles();
        playerIndex = int.Parse(this.gameObject.name);
    }
    void OnMouseDown()
    {
        if (!isSelectable) return;

        MyLogger($" {this.gameObject.name} is ready to move");
        targetPosition = PlayerPrefs.GetInt("DiceRoll", 0);
        MyLogger($" value from PlayerPref {targetPosition}");
        StartCoroutine(StartJump(GetIndex()));

        _pawnSelector.DisableSelection(playerIndex - 1);
        selectionIndication.Stop();
    }

    // Finding Index of the common tile
    private int GetIndex()
    {
        Debug.Log($" get index function is running on{this.gameObject.name}");
        if (inJail)
        {
            inJail = false;
            return startPosition;
        }
        else
        {
            return playerPosition + targetPosition;
        }
    }
    // Turn selection bool off and on
    public void SelectionSwitch(int value)
    {
        isSelectable = value > 0 ? true : false;
        MyLogger($" switch clicked and selection is {isSelectable}");
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
    // Moving to the target index 
    private IEnumerator StartJump(int Index)
    {
        MyLogger($" target index is {Index}");
        do
        {
            Vector3 nextJump = commonTiles[playerPosition].position;
            MyLogger($"current index is {Index} and player index is {playerPosition} ");
            JumpToPosition(nextJump);
            playerPosition++;
            yield return new WaitForSeconds(0.5f);
        }
        while (playerPosition < Index);

        UIManager.Instance.TurnIndication();
        // Inform tile to check for 
        Transform tile = commonTiles[playerPosition + Index];
        TileScript tileScript = tile.GetComponent<TileScript>();
        tileScript.OnPlayerLands(this);
    }

    public void JumpToPosition(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        Vector2 peakPos = new Vector3(startPos.x + targetPos.x / 10f, Mathf.Max(startPos.y, targetPos.y) + jumpHeight);

        Sequence jumpSeq = DOTween.Sequence();

        jumpSeq.Append(transform.DOMove(peakPos, 0.2f).SetEase(Ease.OutQuad));

        jumpSeq.Append(transform.DOMove(targetPos, 0.2f).SetEase(Ease.InQuad));
    }
    private void MyLogger(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}
