using UnityEngine;
using UnityEngine.UI;

public class Settingspanel : MonoBehaviour
{
    [SerializeField]
    private Slider slidervoliume;
    [SerializeField]
    private float defaultaudio = 1;
    [SerializeField]
    private AudioSource Bgaudio;

    private void Start()
    {
        slidervoliume.value = defaultaudio;
        slidervoliume.onValueChanged.AddListener(voliumecontroller);
    }

    public void voliumecontroller(float value)
    {
        Bgaudio.volume = value;
    }
}
