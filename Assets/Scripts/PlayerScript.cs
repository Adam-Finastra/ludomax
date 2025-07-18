using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public bool inJail = true;
    public Transform startPos;

    [SerializeField] int playerIndex;
    public TeamType teamType;
    [SerializeField] int startIndex;
    [SerializeField] bool isDebug;
    public int playerPosition = 0;
    [SerializeField] float jumpHeight = 0.1f;
    [SerializeField] private ParticleSystem selectionIndication;
    private PawnSelector _pawnSelector;
    private List<Transform> commonTiles = new List<Transform>();
    private int teamCount;
    private int targetPosition;
    private int steps = 0;
    public bool isSelectable;

    void Awake()
    {
        _pawnSelector = GetComponentInParent<PawnSelector>();
    }
    void Start()
    {
        commonTiles = TileManager.Instance.CommonTiles();
        playerIndex = int.Parse(this.gameObject.name);
        teamCount = PlayerPrefs.GetInt("TeamCount", 0);
    }
    void OnMouseDown()
    {
        if (!isSelectable) return;

        targetPosition = PlayerPrefs.GetInt("DiceRoll", 0);
        StartCoroutine(StartJump(GetIndex()));

        _pawnSelector.DisableSelection(playerIndex - 1);
        selectionIndication.Stop();
    }

    // Finding Index of the common tile
    private int GetIndex()
    {
        if (inJail)
        {
            inJail = false;
            return startIndex;
        }
        else
        {

            return (playerPosition + targetPosition) % commonTiles.Count;
        }
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
    // Moving to the target index 
    private IEnumerator StartJump(int Index)
    {
        MyLogger($" target index is {Index}");
        do
        {
            Vector3 nextJump = commonTiles[playerPosition].position;
            MyLogger($" index is {Index} and current player index is {playerPosition} ");
            JumpToPosition(nextJump);
            playerPosition++;
            steps++;
            yield return new WaitForSeconds(0.5f);
        }
        while (playerPosition < Index);

        MyLogger($" position after jump is {playerPosition} ");
        // UIManager.Instance.TurnIndication();
        // Inform tile to check for 
        Transform tile = commonTiles[playerPosition - 1];
        TileScript tileScript = tile.GetComponent<TileScript>();
        tileScript.OnPlayerLands(this);
    }

    private void JumpToPosition(Vector3 targetPos)
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
