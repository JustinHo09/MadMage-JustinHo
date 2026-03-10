using UnityEngine;

public class start : MonoBehaviour
{
    public GameObject startScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void startGame()
    {
        // Instantiate the level 0 game object
        GameObject level = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<gameController>().levels[0];
        //Instantiate that in the middle of the game
        startScreen.SetActive(false);
    }
}
