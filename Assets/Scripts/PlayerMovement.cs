using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool isDebug;
    [SerializeField] float jumpHeight = 0.1f;
    [SerializeField] public float jumpSpeed = 0.4f;

    private int tileCount;
    private List<Transform> commonTiles = new List<Transform>();
    private PlayerScript playerScript;
    private TeamScript teamScript;

    [HideInInspector] public int targetSteps;

    void Awake()
    {
        jumpSpeed = 0.4f;
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
        if (playerScript.inhomePath && targetSteps > 56)
        {
            GameEvent.EnableButton?.Invoke();
            yield break;
        }
            
        while (playerScript.steps < targetSteps)
        {
            playerScript.steps++;

            if (playerScript.steps == 51)
            {
                playerScript.playerPosition = 0;
                playerScript.inhomePath = true;
            }

            Vector3 nextJump = NextJump();
            JumpToPosition(nextJump);

            yield return new WaitForSeconds(jumpSpeed);
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
        if (playerScript.steps <= 50)
        {
            playerScript.playerPosition = (playerScript.playerPosition + 1) % tileCount;
            return commonTiles[playerScript.playerPosition].position;
        }
        else
        {
            List<Transform> homeTiles = teamScript.TurnTile();
            Vector3 nextJump = homeTiles[playerScript.playerPosition].position;
            playerScript.playerPosition++;
            return nextJump;
        }
    }

    private Transform GetCurrentTile()
    {
        if (playerScript.steps <= 50)
        {
            return commonTiles[playerScript.playerPosition];
        }
        else
        {
            List<Transform> homeTiles = teamScript.TurnTile();
            return homeTiles[Mathf.Clamp(playerScript.playerPosition - 1, 0, homeTiles.Count - 1)];
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