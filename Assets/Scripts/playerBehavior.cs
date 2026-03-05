using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class playerBehavior : MonoBehaviour
{
    public Slider manaBar;
    public int maxMana;
    public int currentMana;
    
    public GameObject spell;
    public GameObject[] spells;

    public AudioSource cast;
    
    public GameObject gameOver;
    public GameObject victory;

    public TMP_Text score;
    public int points;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMana = maxMana;
        manaBar.maxValue = maxMana;
        manaBar.value = currentMana;
        score.SetText("SCORE: " + points);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the victory condition of no more enemies
        if(GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
        {
            victory.SetActive(true);
            this.enabled = false;
        }
        
        // Checks the first game over condition which is no more mana, only after the last spell is gone.
        if (currentMana == 0 && GameObject.FindGameObjectsWithTag("spell").Length == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
            {
                victory.SetActive(true);
                this.enabled = false;
            }else{
                gameOver.SetActive(true);
                this.enabled = false;
            }
        }
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame 
            && currentMana-spell.GetComponent<spellBehavior>().cost >= 0)
        {
            currentMana -= spell.GetComponent<spellBehavior>().cost;
            manaBar.value = currentMana;
            
            cast.Play();
            
            // Launch the spell
            
            
            
        }
    }

    public void updateScore(int add)
    {
        points += add;
        score.SetText("SCORE: "+points);
    }
    
}
