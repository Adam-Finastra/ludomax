using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private TMP_InputField enteredText;
    public void LoadNextScene()
    {
        string num = enteredText.text;

        if (int.TryParse(num.Trim(), out int number))
        {
            Debug.Log($"Parsed number is {number}");
            PlayerPrefs.SetInt("NumberOfPlayers", number);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log($" cannot convert the entered number");
        }
    }
}
