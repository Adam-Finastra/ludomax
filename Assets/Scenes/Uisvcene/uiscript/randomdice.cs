using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class randomdice : MonoBehaviour
{
    

    List <int> weightList = new List <int>();

    private void Start()
    {
        
        
       

    }




    public  void startroll(int indexss = 0)
    {
        int[] weight = { 1, 1, 1, 1, 1, indexss };
        weightList.Clear();
        for (int i = 0; i < weight.Length; i++)
        {
            for (int j = 0; j < weight[i]; j++)
            {
                weightList.Add(i + 1);
            }
        }
        int randointex  = UnityEngine.Random.Range(0, weightList.Count);
        int rollednumber = weightList[randointex];
        Debug.Log(rollednumber);
    }
}
