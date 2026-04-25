using System.Linq;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject [] levels;
    // Keep track of index of levels visted from levels array
    private int [] visited;
    public int currentLevel;
    private int size;
    public GameObject victory;

    public AudioSource [] breaks;
    public AudioSource death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visited = new int[levels.Length];
        currentLevel = 0;
        size = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void nextLevel()
    {
        visited[size] = currentLevel;
        size++;
        if (size != levels.Length) {
            currentLevel = getNextLevel(currentLevel, 0);
        }
        else
        {
            victory.SetActive(true);
            //Disable all other scripts
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
    
}
