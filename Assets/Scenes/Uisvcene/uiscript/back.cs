using UnityEngine;
using UnityEngine.SceneManagement;
public class back : MonoBehaviour
{
    int backwardindex = 0;
   
   public void backward()
    {

        SceneManager.LoadScene(backwardindex);
    }
  public  void quit()
    {
        Application.Quit();
    }
}
