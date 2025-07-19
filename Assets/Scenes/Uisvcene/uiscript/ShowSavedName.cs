using TMPro;
using UnityEngine;

public class ShowSavedNames : MonoBehaviour
{
    [Header("Player Name Texts")]
    [SerializeField] private TMP_Text [] player1Text;
    int index = 0;
    

    private void Start()
    {
        for (int i = 0; i < player1Text.Length; i++)
        {
            index++;
            string name1 = PlayerPrefs.GetString("player" + i, "");
            if (string.IsNullOrWhiteSpace(name1))
            {
                // If name is empty, fallback to default like "Player0"
                name1 = "Player" + index;
            }


            if (player1Text[i] != null) player1Text[i].text = name1;

        }
    
    }
}
