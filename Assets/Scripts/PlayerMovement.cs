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
    private PlayerScript playerScript;
    private TeamScript teamScript;

    private int targetSteps;

    void Awake()
    {
        teamScript = GetComponentInParent<TeamScript>();
    }

    void Start()
    {
        commonTiles = TileManager.Instance.CommonTiles();
        tileCount = TileManager.Instance.CommonTilesCount();
    }

    public void IntialJumpLoop(PlayerScript player, int stepsToMove)
    {
        playerScript = player;
        targetSteps = playerScript.steps + stepsToMove;
        StartCoroutine(StartJump());
    }

    private IEnumerator StartJump()
    {
        while (playerScript.steps < targetSteps)
        {
            playerScript.steps++;

            if (playerScript.steps == 50)
            {
                playerScript.playerPosition = 0;
            }

            Vector3 nextJump = NextJump();
            JumpToPosition(nextJump);

            yield return new WaitForSeconds(0.5f);
        }

        Transform tile = GetCurrentTile();
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

    private Vector3 NextJump()
    {
        if (playerScript.steps >= 50)
        {
            List<Transform> homeTiles = teamScript.TurnTile();
            Vector3 nextJump = homeTiles[playerScript.playerPosition].position;
            playerScript.playerPosition++;
            return nextJump;
        }
        else
        {
            playerScript.playerPosition = (playerScript.playerPosition + 1) % tileCount;
            return commonTiles[playerScript.playerPosition].position;
        }
    }

    private Transform GetCurrentTile()
    {
        if (playerScript.steps >= 50)
        {
            List<Transform> homeTiles = teamScript.TurnTile();
            return homeTiles[Mathf.Clamp(playerScript.playerPosition - 1, 0, homeTiles.Count - 1)];
        }
        else
        {
            return commonTiles[playerScript.playerPosition];
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