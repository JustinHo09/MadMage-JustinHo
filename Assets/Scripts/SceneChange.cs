using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void toEasy()
    {
        SceneManager.LoadScene(1);
    }
    
    public void toMedium()
    {
        SceneManager.LoadScene(2);
    }

    public void toHard()
    {
        SceneManager.LoadScene(3);
    }
}
