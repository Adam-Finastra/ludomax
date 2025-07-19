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
    [SerializeField]
    private string[] playernamesarray = new string[4];

    [Header("Settings")]
    

    private bool panelOn = true;
    bool hasname = false;
   

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
            if (playername[i].text != null && !hasname)
            {
                PlayerPrefs.SetString("player" + i, playername[i].text);
                PlayerPrefs.Save();
            
            }
            else
            {
                Debug.Log($"player is empty now");
            }
           
               
        }
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        Debug.Log("the next scene is loaded");
        if(PlayerPrefs.GetString("player","") != null)
        {

            Debug.Log(PlayerPrefs.GetString("player", "") + "playernameisempty");
            SceneManager.LoadScene(PlayerPrefs.GetInt("Index", 0));
        }
        else
        {
            Debug.Log($"the input area is null{PlayerPrefs.GetString("player","")} ");
            return;
            
        }
      
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
