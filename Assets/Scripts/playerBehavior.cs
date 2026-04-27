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

    public TMP_Text score;
    public int points;
    public int oldPoint;

    private int lowestCost;
    
    public Animator animations;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            nextLevel.SetActive(true);
            endScore.SetText("SCORE: "+points);
            animations.SetTrigger("Win");
            this.enabled = false;
            GetComponentInChildren<LauncherBehavior>().enabled = false;
            oldPoint = points;
        }
        
        // Checks the first game over condition which is no more mana, only after the last spell is gone.
        // Also check to make sure that the lowest spell cost - current mana >=0 otherwise it would be
        // impossible to win thus being a loss
        if (currentMana - lowestCost < 0 && GameObject.FindGameObjectsWithTag("Spell").Length == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
            {
                nextLevel.SetActive(true);
                this.enabled = false;
                GetComponentInChildren<LauncherBehavior>().enabled = false;
                oldPoint = points;
            }else{
                GameObject.FindGameObjectWithTag("Launcher").GetComponent<LauncherBehavior>().warningSign.SetActive(false);
                gameOver.SetActive(true);
                GetComponentInChildren<LauncherBehavior>().enabled = false;
                animations.SetTrigger("Lose");
                this.enabled = false;
            }
        }

        
    }

    public void updateScore(int add)
    {
        points += add;
        score.SetText("SCORE: "+points);
    }
    
    
    public void updateMana(int manaChange)
    {
        currentMana += manaChange;
        manaBar.value = currentMana;
    }
    
    public void restart()
    {
        points = oldPoint;
        score.SetText("SCORE: "+points);
        currentMana = maxMana;
        manaBar.value = currentMana;
        gameOver.SetActive(false);
        animations.SetTrigger("Restart");
        GetComponentInChildren<LauncherBehavior>().enabled = true;
        this.enabled = true;
        nextLevel.SetActive(false);
    }
    
}
