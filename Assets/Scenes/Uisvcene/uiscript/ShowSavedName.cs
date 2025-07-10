using TMPro;
using UnityEngine;

public class ShowSavedNames : MonoBehaviour
{
    [Header("Player Name Texts")]
    [SerializeField] private TMP_Text [] player1Text;
    

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            string name1 = PlayerPrefs.GetString("player" + i, "Empty");
            if (player1Text != null) player1Text[i].text = name1;
            Debug.Log($" players pref is working {player1Text[i].text} ");   
        }
    
    }
}
