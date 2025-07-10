using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnableInput : MonoBehaviour
{
    [SerializeField]
    private GameObject[]twopperson;


   public void allpersonenable(int index)
    {
        for (int i = 0; i < twopperson.Length; i++)
        {
           if( twopperson[i] != null)
            {
                twopperson[i].SetActive(i < index);
            }
            
        }
        PlayerPrefs.SetInt("Index", index - 1);

    }
   
}
