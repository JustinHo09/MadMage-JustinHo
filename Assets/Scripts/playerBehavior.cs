using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class playerBehavior : MonoBehaviour
{
    public Slider manaBar;
    public int maxMana;
    public int currentMana;
    
    //public GameObject spell;
    // public GameObject[] spells;

    public AudioSource cast;
    
    public GameObject gameOver;
    public GameObject victory;

    public TMP_Text score;
    public int points;

    private int lowestCost;
    
    public Animator animations;
    
    // private Camera cam;
    // public float multiplier;
    // public float maxDrag;
    // private Vector2 startPos;
    //private int currentSpell;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentSpell=0;
        //spell = Instantiate(spells[currentSpell],new Vector3(transform.position.x+2.0f, transform.position.y+3.0f,0.0f),Quaternion.identity);
        currentMana = maxMana;
        manaBar.maxValue = maxMana;
        manaBar.value = currentMana;
        score.SetText("SCORE: " + points);
        lowestCost = GameObject.FindGameObjectWithTag("Launcher").GetComponent<LauncherBehavior>().lowestCost;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the victory condition of no more enemies
        if(GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
        {
            victory.SetActive(true);
            animations.SetTrigger("Win");
            this.enabled = false;
        }
        
        // Checks the first game over condition which is no more mana, only after the last spell is gone.
        // Also check to make sure that the lowest spell cost - current mana >=0 otherwise it would be
        // impossible to win thus being a loss
        if (currentMana - lowestCost < 0 && GameObject.FindGameObjectsWithTag("Spell").Length == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
            {
                victory.SetActive(true);
                this.enabled = false;
            }else{
                GameObject.FindGameObjectWithTag("Launcher").GetComponent<LauncherBehavior>().warningSign.SetActive(false);
                gameOver.SetActive(true);
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
    
}
