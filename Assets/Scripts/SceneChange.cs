using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int toScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toScene = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void toGame()
    {
        SceneManager.LoadScene(toScene);
    }
    
    public void right()
    {
        toScene += 1;
        if (toScene >= 4)
        {
            toScene = 1;
        }
    }

    public void left()
    {
        toScene -= 1;
        if (toScene <= 0)
        {
            toScene = 3;
        }
    }
}
