using UnityEngine;
using UnityEngine.UI;

public class ShowSaved : MonoBehaviour
{
    [SerializeField] private Image[] playerImages;      // UI Images shown on screen
    [SerializeField] private Sprite[] spriteOptions;    // All available sprites to assign

    void Start()
    {
        LoadAllPlayerImages();
    }

    void LoadAllPlayerImages()
    {
        for (int i = 0; i < playerImages.Length; i++)
        {
            int index = PlayerPrefs.GetInt("playerimage_" + i, 0);
            index = Mathf.Clamp(index, 0, spriteOptions.Length - 1);

            if (playerImages[i] != null)
                playerImages[i].sprite = spriteOptions[index];
        }
    }

    // Call this if you want to manually save a sprite index for a player
    public void SavePlayerImage(int playerIndex, int spriteIndex)
    {
        PlayerPrefs.SetInt("playerimage_" +playerIndex , spriteIndex);
        
    }
}
