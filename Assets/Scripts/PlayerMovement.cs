using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool isDebug;
    [SerializeField] float jumpHeight = 0.1f;
    private int tileCount;
    private List<Transform> commonTiles = new List<Transform>();
    private TeamScript teamScript;
    private PlayerScript playerScript;
    void Awake()
    {
        teamScript = GetComponentInParent<TeamScript>();
    }
    void Start()
    {
        commonTiles = TileManager.Instance.CommonTiles();
        tileCount = TileManager.Instance.CommonTilesCount();
    }
    public void IntialJumpLoop(PlayerScript player, int index)
    {
        playerScript = player;
        StartCoroutine(StartJump(index));
    }
    // Moving to the target index 
    private IEnumerator StartJump(int Index)
    {
        MyLogger($" target index is {Index}");
        do
        {
            playerScript.steps++;
            Vector3 nextJump = NextJump(playerScript.steps);
            MyLogger($" index is {Index} and current player index is {playerScript.steps} ");
            JumpToPosition(nextJump);
            yield return new WaitForSeconds(0.5f);
        }
        while (playerScript.steps < Index);

        MyLogger($" position after jump is {playerScript.playerPosition}  and steps { playerScript.steps}");

        // Inform tile to check for 
        Transform tile = commonTiles[playerScript.playerPosition];
        TileScript tileScript = tile.GetComponent<TileScript>();
        tileScript.OnPlayerLands(playerScript);
    }

    public void JumpToPosition(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        Vector2 peakPos = new Vector3(startPos.x + targetPos.x / 10f,
                            Mathf.Max(startPos.y, targetPos.y) + jumpHeight);

        Sequence jumpSeq = DOTween.Sequence();

        jumpSeq.Append(transform.DOMove(peakPos, 0.2f).SetEase(Ease.OutQuad));

        jumpSeq.Append(transform.DOMove(targetPos, 0.2f).SetEase(Ease.InQuad));
    }
    private Vector3 NextJump(int stepCount)
    {
        if (stepCount >= 50)
        {
            List<Transform> tile = teamScript.TurnTile();
            Vector3 nextJump = tile[playerScript.playerPosition].position;
            return nextJump;
        }
        else
        {
            playerScript.playerPosition = (playerScript.playerPosition + 1) % tileCount;
            Vector3 nextJump = commonTiles[playerScript.playerPosition].position;
            return nextJump;
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
