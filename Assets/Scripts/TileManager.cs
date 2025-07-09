using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }
    [SerializeField] GameObject commonTile;
    [SerializeField] GameObject blueTile;
    [SerializeField] GameObject redTile;
    [SerializeField] GameObject greenTile;
    [SerializeField] GameObject yellowTile;

    private List<Transform> commonTiles = new List<Transform>();
    private List<Transform> blueTiles = new List<Transform>();
    private List<Transform> redTiles = new List<Transform>();
    private List<Transform> greenTiles = new List<Transform>();
    private List<Transform> yellowTiles = new List<Transform>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        commonTiles = TransformsFromChildren(commonTile);
        blueTiles = TransformsFromChildren(blueTile);
        redTiles = TransformsFromChildren(redTile);
        yellowTiles = TransformsFromChildren(yellowTile);
        greenTiles = TransformsFromChildren(greenTile);
    }

    public List<Transform> CommonTiles()
    {
        return commonTiles;
    }
    public List<Transform> BlueTiles()
    {
        return blueTiles;
    }
    public List<Transform> RedTiles()
    {
        return redTiles;
    }
    public List<Transform> GreenTiles()
    {
        return greenTiles;
    }
    public List<Transform> YellowTiles()
    {
        return yellowTiles;
    }

    private List<Transform> TransformsFromChildren(GameObject gameObj)
    {
        Transform[] allTransforms = gameObj.GetComponentsInChildren<Transform>();
        List<Transform> rr = new List<Transform>();
        foreach (Transform item in allTransforms)
        {
            if (item != gameObj.transform)
            {
                rr.Add(item);
            }
        }
        return rr;
    }
}
