using System.Linq;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject [] levels;
    // Keep track of index of levels visted from levels array
    private int [] visited;
    private int currentLevel;

    public AudioSource [] breaks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visited = new int[levels.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void nextLevel()
    {
        
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
    
    public void restart()
    {
        
    }
}
