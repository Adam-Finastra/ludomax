using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField]
    public RectTransform popuppanel;
    bool popou = false;
    float time = 3f;
    [SerializeField]
    private ParticleSystem confettiee;
    private void Start()
    {
        popuppanel.localScale = Vector3.zero;
        popuppanel.gameObject.SetActive(false);
    }
    private void Update()
    {
       
    }
    public void showpopup()
    {
      popuppanel.gameObject.SetActive(true);
      popuppanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
      time = Time.deltaTime;
      confettiee.DOPlay();
    }
   public void hidepopup()
    {
        
        
            popuppanel.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
          .OnComplete(() => popuppanel.gameObject.SetActive(false));
        
   
    }
 
}
