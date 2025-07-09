using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        
    }
}
