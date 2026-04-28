using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int toScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        toScene = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Go to main menu scene
    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    // Go the one of hte game scenes
    public void toGame()
    {
        SceneManager.LoadScene(toScene);
        Time.timeScale = 1;
    }
    
    // Move the right on difficulty selector
    public void right()
    {
        toScene += 1;
        if (toScene >= 4)
        {
            toScene = 1;
        }
    }

    //Move to the left on the difficulty selector
    public void left()
    {
        toScene -= 1;
        if (toScene <= 0)
        {
            toScene = 3;
        }
    }
}
