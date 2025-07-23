using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class sfx : MonoBehaviour
{
    public static sfx sfxinstance;
    [SerializeField]
    private AudioSource effectsaudiosource;
    [SerializeField]
    private AudioClip diceclip;
    [SerializeField]
    private AudioClip jumpclip;
   // [SerializeField]
  //  private AudioClip winningclip;
    [SerializeField]
    private AudioSource Bgaudio;
    [SerializeField]
    private Slider Bgvoliumeslider;
    [SerializeField]
    private Slider effectsvoliumelider;
    float masteraudio = 0;
    float effectsaudio = 0;


    private void Awake()
    {
        if (sfxinstance != null && sfxinstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        sfxinstance = this;
    }
    private void Start()
    {
        masteraudio = PlayerPrefs.GetFloat("voliume", 0f);
        Bgaudio.volume = masteraudio;
        Bgvoliumeslider.value = masteraudio;
        Debug.Log(PlayerPrefs.GetFloat("voliume", 0f) + "bg audio");
        
        effectsaudio = PlayerPrefs.GetFloat("effects", 0f);
        effectsaudiosource.volume = effectsaudio;
        effectsvoliumelider.value = effectsaudio;   
        Debug.Log(PlayerPrefs.GetFloat("effects", 0f) + "effects audio");
       
    }

    public void dicesfx()
    {
        effectsaudiosource.PlayOneShot(diceclip);
      
    }
    public void jumpsound()
    {
        effectsaudiosource.PlayOneShot(jumpclip);
    }
    public void winning()
    {
        //effectsaudiosource.PlayOneShot(winningclip);
    }
    public void mastervoliume()
    {
        masteraudio = Bgvoliumeslider.value;
        Bgaudio.volume = masteraudio;
        PlayerPrefs.SetFloat("voliume", masteraudio);
        PlayerPrefs.Save();
   
    }
    public void effectsvoliume()
    {
        effectsaudio = effectsvoliumelider.value;
        effectsaudiosource.volume = effectsaudio;
        PlayerPrefs.SetFloat("effects", effectsaudio);
        PlayerPrefs.Save();
    }
}
