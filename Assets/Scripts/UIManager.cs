using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject[] TeamUI;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

    }
    
    public void TurnIndication(int index)
    {
        for (int i = 0; i < TeamUI.Length; i++)
        {
            TeamUI[i].SetActive(i == index);
        }
    }

}
