using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System.Linq.Expressions;

public class Popup : MonoBehaviour
{
    [SerializeField]
    public RectTransform popuppanel;
    bool popou = false;
    float time = 3f;
    [SerializeField]
    private ParticleSystem confettiee;
    [SerializeField]
    private RectTransform options;
    bool optionison = false;
    private void Start()
    {
        popuppanel.localScale = Vector3.zero;
        popuppanel.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
    }
    
    public void showpopup()
    {
      popuppanel.gameObject.SetActive(true);
      popuppanel.DOScale(new Vector3(1.194727f, 1.194727f, 1.194727f), 0.5f).SetEase(Ease.OutBack);
      StartCoroutine(setting_timescale_to_zero());
     
    }
   public void hidepopup()
    {
     popuppanel.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
     .OnComplete(() => popuppanel.gameObject.SetActive(false));
     Time.timeScale = 1;
        options.gameObject.SetActive(false);

    }
   void OnMouseDown()
    {
        hideoption();
        Debug.Log("on mouse down botton is working");
    }
    IEnumerator setting_timescale_to_zero()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
    }
    public void showoptions()
    {
        
      options.gameObject.SetActive(true);

      options.anchoredPosition = new Vector2(6, options.anchoredPosition.y);
      options.localScale = Vector3.zero;


       options.DOAnchorPosX(-166.89f, 0.5f).SetEase(Ease.OutBack);
       options.DOScale(new Vector3(0.472f, 0.472f, 1), 0.5f).SetEase(Ease.OutBack);
       optionison = true;
        
       
    }
    public void hideoption()
    {
        options.gameObject.SetActive(false);
        
    }



 
}
