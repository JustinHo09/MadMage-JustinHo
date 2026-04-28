using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class gameController : MonoBehaviour
{
    // Keep track of all levels and the current one
    public GameObject [] levels;
    public GameObject level;
    // Keep track of index of levels visted from levels array
    private int [] visited;
    
    // KMeep track of current level index
    public int currentLevel;
    private int size;
    
    // Ui componenets
    public GameObject victory;
    public TMP_Text score;
    
    
    public GameObject player;
    
    // Audiosources for blocks and dragons
    public AudioSource [] breaks;
    public AudioSource death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visited = new int[levels.Length];
        currentLevel = 0;
        size = 0;
        // Create the tutorial level
        level = Instantiate(levels[0], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //  Cheat code to move to the next level
        if(Keyboard.current.zKey.wasPressedThisFrame)
        {
            if(Keyboard.current.pKey.wasPressedThisFrame){
                nextLevel();
            }
        }
    }
    
    public void nextLevel()
    {
        // Get rid of everything from thr current level
        Destroy(level);
        // Update visited levels with  the current one and increment size by  one
        visited[size] = currentLevel;
        size++;
        // if you have not visited all levels then make the new one
        if (size != levels.Length) {
            // Generate a random level that has not been visited yet
            currentLevel = getNextLevel(currentLevel, 0);
            // Make the level, disable the next level ui, call palyer restart, and enable the launcher
            level = Instantiate(levels[currentLevel], transform.position, Quaternion.identity);
            player.GetComponent<playerBehavior>().nextLevel.SetActive(false);
            player.GetComponent<playerBehavior>().restart();
            player.GetComponentInChildren<LauncherBehavior>().enabled = true;
        }
        else
        {
            // If all levels have been  visited then enable vistory and display points earned
            victory.SetActive(true);
            score.SetText("SCORE: "+player.GetComponent<playerBehavior>().points);
            // Disable the player scripts, ui , annd launcher
            player.GetComponent<playerBehavior>().enabled = false;
            player.GetComponent<playerBehavior>().nextLevel.SetActive(false);
            player.GetComponentInChildren<LauncherBehavior>().enabled = false;
        }
    }

    // Generate the nextlevel by runing it through a function that uses a random nnumber
    // and does modulo by the level length to ensure we get a valid level
    private int getNextLevel(int current, int count)
    {
        int nextLevel = (current+Random.Range(1,100)+count)% levels.Length;
        // if we have already visited the level then call this method again and increase count by one
        // this way the odds of visited hte same level are low.
        if(visited.Contains(nextLevel))
        {
            nextLevel = getNextLevel(current,count+1);
        }
        return nextLevel;
    }
    
    // This will restart the current level the player is on my destorying the level
    // and cre creating it, and destroying all remaining spells on field
    public void restartLevel()
    {
        Destroy(level);
        Destroy(GameObject.FindGameObjectWithTag("Spell"));
        level = Instantiate(levels[currentLevel], transform.position, Quaternion.identity);
        // call players restart method to restart their mana and score
        player.GetComponent<playerBehavior>().restart();
    }
    
}
