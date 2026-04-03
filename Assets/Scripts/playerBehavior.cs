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
    
    public Animator animations;
    
    private Camera cam;
    public float multiplier;
    public float maxDrag;
    private Vector2 startPos;
    private int currentSpell;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpell=0;
        spell = Instantiate(spells[currentSpell],new Vector3(transform.position.x+2.0f, transform.position.y+3.0f,0.0f),Quaternion.identity);
        cam = Camera.main;
        currentMana = maxMana;
        manaBar.maxValue = maxMana;
        manaBar.value = currentMana;
        score.SetText("SCORE: " + points);
    }

    // Update is called once per frame
    void Update()
    {
        if(spell == null)
        {
            spell = Instantiate(spells[currentSpell],new Vector3(transform.position.x+2.0f, transform.position.y+3.0f,0.0f),Quaternion.identity);
        }
        // Checks the victory condition of no more enemies
        if(GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
        {
            victory.SetActive(true);
            animations.SetTrigger("Win");
            this.enabled = false;
        }
        
        // Checks the first game over condition which is no more mana, only after the last spell is gone.
        // Also check to make sure that the lowest spell cost - current mana >=0 other wise it would be
        // impossible to win thus being a loss
        if (currentMana == 0 && GameObject.FindGameObjectsWithTag("Spell").Length == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Dragon").Length == 0)
            {
                victory.SetActive(true);
                this.enabled = false;
            }else{
                gameOver.SetActive(true);
                animations.SetTrigger("Lose");
                this.enabled = false;
            }
        }

        if (currentMana - spell.GetComponent<spellBehavior>().cost >= 0)
        {
            // Mouse click down so it is starting pos
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePos = Mouse.current.position.value;
                startPos = cam.ScreenToWorldPoint(mousePos);
            }

            // Mouse drag to get every current position
            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 mousePos = Mouse.current.position.value;
                Vector2 currentPos = cam.ScreenToWorldPoint(mousePos);
                // make sure currentPos does not excede bounds
                if ((startPos.x - currentPos.x > maxDrag || startPos.x - currentPos.x < -maxDrag) &&
                    (startPos.y - currentPos.y > maxDrag || startPos.y - currentPos.y < -maxDrag)) {
                    if (startPos.x - currentPos.x > maxDrag)
                    {
                        currentPos.x = startPos.x + maxDrag;
                    }
                    else if (startPos.x - currentPos.x < -maxDrag)
                    {
                        currentPos.x = startPos.x - maxDrag;
                    }

                    if (startPos.y - currentPos.y > maxDrag)
                    {
                        currentPos.y = startPos.y + maxDrag;
                    }
                    else if (startPos.y - currentPos.y < -maxDrag)
                    {
                        currentPos.y = startPos.y - maxDrag;
                    }
                }
            }

            // Mouse release to get final position
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                currentMana -= spell.GetComponent<spellBehavior>().cost;
                manaBar.value = currentMana;

                cast.Play();
                animations.SetTrigger("Attack");
                Vector2 mousePos = Mouse.current.position.value;
                Vector2 endPos = cam.ScreenToWorldPoint(mousePos);
                Vector2 force = (startPos - endPos) * multiplier;

                spell.GetComponent<Rigidbody2D>().gravityScale = 2.5f;
                spell.GetComponent<Collider2D>().enabled = true;
                spell.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    public void updateScore(int add)
    {
        points += add;
        score.SetText("SCORE: "+points);
    }
    
    public void updateSpell(int newSpell)
    {
        currentSpell= newSpell;
        Destroy(spell);
        spell = Instantiate(spells[newSpell],new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        spell.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        spell.GetComponent<Collider2D>().enabled = true;
    }
    
}
