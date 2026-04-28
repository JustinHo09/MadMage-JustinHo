using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class playerBehavior : MonoBehaviour
{
    public Slider manaBar;
    public int maxMana;
    public int currentMana;
    


    public AudioSource cast;
    
    public GameObject gameOver;
    public GameObject nextLevel;
    public TMP_Text endScore;
    public TMP_Text loseScore;

    public TMP_Text score;
    public int points;
    public int oldPoint;

    private int lowestCost;
    
    public Animator animations;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        currentMana = maxMana;
        manaBar.maxValue = maxMana;
        manaBar.value = currentMana;
        score.SetText("SCORE: " + points);
        oldPoint = 0;
        lowestCost = GameObject.FindGameObjectWithTag("Launcher").GetComponent<LauncherBehavior>().lowestCost;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the victory condition of no more enemies
        if(GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
        {
            // Set the next level sign active and update the point board.
            nextLevel.SetActive(true);
            endScore.SetText("SCORE: "+points);
            // Play the win animation and diable the script for htis and the launcher
            animations.SetTrigger("Win"); 
            this.enabled = false;
            GetComponentInChildren<LauncherBehavior>().enabled = false;
            // Make th old points this points
            oldPoint = points;
        }
        
        // Checks the first game over condition which is no more mana, only after the last spell is gone.
        // Also check to make sure that the lowest spell cost - current mana >=0 otherwise it would be
        // impossible to win thus being a loss
        if (currentMana - lowestCost < 0 && GameObject.FindGameObjectsWithTag("Spell").Length == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
            {
                // Enable the next level ui and update score, while playing the win animation
                nextLevel.SetActive(true);
                endScore.SetText("SCORE: "+points);
                animations.SetTrigger("Win");
                // diable this and luancher script while updating old points to thr the currnet one
                this.enabled = false;
                GetComponentInChildren<LauncherBehavior>().enabled = false;
                oldPoint = points;
            }else{
                // Case of player lsoing
                //Disable the warning sign, enable the game over message
                GameObject.FindGameObjectWithTag("Launcher").GetComponent<LauncherBehavior>().warningSign.SetActive(false);
                gameOver.SetActive(true);
                // Show the player their score at of now and disable the launcher
                loseScore.SetText("SCORE: " + points);
                GetComponentInChildren<LauncherBehavior>().enabled = false;
                // Trigger lose animation and disbale the script
                animations.SetTrigger("Lose");
                this.enabled = false;
            }
        }

        
    }

    // Updates the player score to the input  and also changes the score text
    public void updateScore(int add)
    {
        points += add;
        score.SetText("SCORE: "+points);
    }
    
    // Change the player's mana by the input and update the manabar accordingly
    public void updateMana(int manaChange)
    {
        currentMana += manaChange;
        manaBar.value = currentMana;
    }
    
    //  This is the player's restart make it back to default
    public void restart()
    {
        // Brings points back to original to prevent point farming and update teh score accordingly
        points = oldPoint;
        score.SetText("SCORE: "+points);
        
        // Update the player's mana back to the maximum value and also update the mana bar
        currentMana = maxMana;
        manaBar.value = currentMana;
        // Diable the gameover screen if it was a restart caused by losing
        gameOver.SetActive(false);
        // Go back to the original animation and reenable the launcher
        animations.SetTrigger("Restart");
        GetComponentInChildren<LauncherBehavior>().enabled = true;
        // renable the script and diable the next level ui if restart in preperation fornext level
        this.enabled = true;
        nextLevel.SetActive(false);
    }
    
}
