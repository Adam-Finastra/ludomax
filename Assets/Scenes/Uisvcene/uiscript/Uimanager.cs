using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using Unity.VisualScripting;

public class Uimanager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject localPanel;
    [SerializeField] private GameObject selectionPanel;

    [Header("Default Player Names")]
    [SerializeField] private TMP_InputField [] playername ;
    


    private bool panelOn = true;
    private string[] n = new string[4];
    int b = 0;
   

    private void Start()
    {
        if (localPanel != null && selectionPanel != null)
        {
            localPanel.SetActive(panelOn);
            selectionPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Panels not assigned in inspector!");
        }
       
    }
   

    public void SaveAssignedNamesAndLoadNext()
    {
        
        for (int i = 0; i < 4; i++)
        {
           
            PlayerPrefs.SetString("player" + i, playername[i].text);
            PlayerPrefs.Save();
        
        }
     
        
       

        Debug.Log("Saved assigned names to PlayerPrefs.");
       LoadNextScene();
    }

    private void LoadNextScene()
    {

        SceneManager.LoadScene((PlayerPrefs.GetInt("Index", 0)));
    }

    public void ToggleSelectionPanel()
    {
        if (localPanel != null && selectionPanel != null)
        {
            panelOn = !panelOn;
            localPanel.SetActive(panelOn);
            selectionPanel.SetActive(true);
        }
    }
}
