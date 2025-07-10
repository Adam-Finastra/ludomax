using UnityEngine;
using UnityEngine.SceneManagement;
public class back : MonoBehaviour
{
    int backwardindex = 0;
    private void Start()
    {
      
    }
   public void backward()
    {

        SceneManager.LoadScene(backwardindex);
    }
}
