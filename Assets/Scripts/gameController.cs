using System.Linq;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject [] levels;
    public GameObject level;
    // Keep track of index of levels visted from levels array
    private int [] visited;
    public int currentLevel;
    private int size;
    public GameObject victory;
    public TMP_Text score;
    public GameObject player;
    

    public AudioSource [] breaks;
    public AudioSource death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visited = new int[levels.Length];
        currentLevel = 0;
        size = 0;
        level = Instantiate(levels[0], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void nextLevel()
    {
        Destroy(level);
        visited[size] = currentLevel;
        size++;
        if (size != levels.Length) {
            currentLevel = getNextLevel(currentLevel, 0);
            level = Instantiate(levels[currentLevel], transform.position, Quaternion.identity);
            player.GetComponent<playerBehavior>().nextLevel.SetActive(false);
            player.GetComponent<playerBehavior>().restart();
            player.GetComponentInChildren<LauncherBehavior>().enabled = true;
        }
        else
        {
            victory.SetActive(true);
            score.SetText("SCORE: "+player.GetComponent<playerBehavior>().points);
            player.GetComponent<playerBehavior>().enabled = false;
            player.GetComponent<playerBehavior>().nextLevel.SetActive(false);
            player.GetComponentInChildren<LauncherBehavior>().enabled = false;
        }
    }

    private int getNextLevel(int current, int count)
    {
        int nextLevel = (current+Random.Range(1,100)+count)% levels.Length;
        if(visited.Contains(nextLevel))
        {
            nextLevel = getNextLevel(current,count+1);
        }
        return nextLevel;
    }
    
    public void restartLevel()
    {
        Destroy(level);
        Destroy(GameObject.FindGameObjectWithTag("Spell"));
        level = Instantiate(levels[currentLevel], transform.position, Quaternion.identity);
        player.GetComponent<playerBehavior>().restart();
    }
    
}
