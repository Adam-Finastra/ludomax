using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject diceAnimation;
    [SerializeField] GameObject diceObjects;
    [SerializeField] bool isDebug = true;
    private List<int> weightList = new List<int>();
    private int rolledTimes = 0;
    private int lastValue;
    private Image[] diceSprites;
    private Button button;
    public static Action<int> DiceRoll;
    bool isdebug = false;
   
    
    void Awake()
    {
        button = GetComponent<Button>();
    }
    void OnEnable()
    {
        button.enabled = true;
        GameEvent.EnableButton += ChangeButton;
    }
    void OnDisable()
    {
        GameEvent.EnableButton -= ChangeButton;
    }
    void Start()

    {
       
        diceAnimation.SetActive(false);
        diceSprites = diceObjects.GetComponentsInChildren<Image>();

        foreach (var item in diceSprites)
        {
            item.enabled = false;
        }
    }

    public void StartDice()
    {
        if (!PhotonNetwork.IsConnected || (!photonView.IsMine))
        {
            return;
        }
        photonView.RPC(nameof(RPC_startdiceroll), RpcTarget.All);
    }
    [PunRPC]
   public void RPC_startdiceroll()
    {
        StartCoroutine(RollDice());
        Debug.Log("started the diceroll");
    }

    private IEnumerator RollDice()
    {
        diceAnimation.SetActive(true);
        sfx.sfxinstance.dicesfx();

        showingThesavedname.instance.transferownership();

        yield return new WaitForSeconds(1f);
       
        diceAnimation.SetActive(false);
       

        int rolledValue = DiceBase.rollValue; // fallback or debug value

        if (!DiceBase.IsDebug)
        {
            
            if (photonView.IsMine && !isdebug)
            {
                rolledValue = UnityEngine.Random.Range(1, 7);
                DiceBase.rollValue = rolledValue;
               
               

                Log($"dice rolled :{rolledValue} & probability tracker is {rolledTimes}");

                // Send correct roll to all clients
                photonView.RPC(nameof(RPC_ShowResult), RpcTarget.All, rolledValue);
                button.enabled = false; 
            }
           

        
            DiceRoll?.Invoke(DiceBase.rollValue);
            Log($"dice rolled :{DiceBase.rollValue} & probablity tracker is {rolledTimes}");
        }  
    }
    [PunRPC]
    void RPC_ShowResult(int value)
    {
        ShowDice(value - 1);
        Debug.Log($"Dice rolled: {value}");
    }

    public void ChangeButton()
    {
        Log($" a pawn in request to change the button");
        // button.enabled = button.enabled == true ? false : true;
        button.enabled = true;
    }
    private void ShowDice(int value)
    {
        for (int i = 0; i < diceSprites.Length; i++)
        {
            diceSprites[i].enabled = (i == value);
        }
    }
    private void Log(string message)
    {
        if (isDebug)
        {
            Debug.Log(message);
        }
    }
}