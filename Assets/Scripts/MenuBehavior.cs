using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        Time.timeScale = 0;
    }
    
    public void compensate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<playerBehavior>().updateMana
            (player.GetComponentInChildren<LauncherBehavior>().spell.GetComponent<spellBehavior>().cost);
    }
    
    
    public void unpause()
    {
        Time.timeScale = 1;
    }
}
