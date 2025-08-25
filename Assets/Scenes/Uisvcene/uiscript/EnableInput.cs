using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class EnableInput : MonoBehaviour
{
    [SerializeField]
    private GameObject[] twopperson;


    public void allpersonenable(int index)
    {
        
        if(index != null)
        {
            for (int i = 0; i < twopperson.Length; i++)
            {
                if (twopperson[i] != null)
                {
                    twopperson[i].SetActive(i < index);
                }

            }
            PlayerPrefs.SetInt("NumberOfPlayers", index);
          
        }
        else
        {
            Debug.Log("the index is null");
        }
       
    }
   

}