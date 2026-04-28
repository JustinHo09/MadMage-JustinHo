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

    // Pauses the game by setting tiem scale to 0
    public void pause()
    {
        Time.timeScale = 0;
    }
    
    // Make up for mana lost by clicking on meu buttons by giving them that mana back
    public void compensate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<playerBehavior>().updateMana
            (player.GetComponentInChildren<LauncherBehavior>().spell.GetComponent<spellBehavior>().cost);
    }
    
    
    
    // unpause tiem scale by setting it to 1
    public void unpause()
    {
        Time.timeScale = 1;
    }
}
