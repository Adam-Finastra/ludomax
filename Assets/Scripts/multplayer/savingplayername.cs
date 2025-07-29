using TMPro;
using UnityEngine;

public class savingplayername : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nickname;
    public void savenickname()
    {
        PlayerPrefs.SetString("nickname",nickname.text);
        PlayerPrefs.Save();
    }
}
