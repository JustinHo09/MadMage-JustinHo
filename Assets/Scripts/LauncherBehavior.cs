using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class LauncherBehavior : MonoBehaviour
{
    public float gravityScale;
    public bool isCasting;
    
    public GameObject spell;
    public GameObject oldSpell;
    public GameObject[] spells;
    public int currentSpell;

    public int lowestCost;
    
    private Camera cam;
    public float multiplier;
    public float maxDrag;
    private Vector2 startPos;
    
    public GameObject warningSign;
    public TMP_Text castable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCasting = false;
        currentSpell=0;
        cam = Camera.main;
        spell = Instantiate(spells[currentSpell],transform.position,Quaternion.identity);
        lowestCost = spells[0].GetComponent<spellBehavior>().cost;
        for (int i = 1; i < spells.Length; i++)
        {
            if (spells[i].GetComponent<spellBehavior>().cost < lowestCost)
            {
                lowestCost = spells[i].GetComponent<spellBehavior>().cost;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentMana = GetComponentInParent<playerBehavior>().currentMana;
        
        // only create the spell if there is none and there is enough mana to cast it
        if (spell == null && oldSpell == null && currentMana - lowestCost >= 0)  
        {
            spell = Instantiate(spells[currentSpell],transform.position,Quaternion.identity);
        }else if(currentMana - spells[currentSpell].GetComponent<spellBehavior>().cost < 0)
        {
            warningSign.SetActive(true);
            castable.SetText("Not Enough Mana To Cast This Spell, Try Another One");
        }

        if (isCasting == false)
        {
            // left arrow
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
            {
                currentSpell--;
                if (currentSpell < 0)
                {
                    currentSpell = spells.Length-1;
                }
                updateSpell(currentSpell);
            }
            
            //right arrow
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
            {
                currentSpell++;
                currentSpell = currentSpell % spells.Length;
                updateSpell(currentSpell);
            }
        }
        
        if(spell!=null){
            if (currentMana - spell.GetComponent<spellBehavior>().cost >= 0)
            {
                // Mouse click down so it is starting pos
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    isCasting = true;
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
                        (startPos.y - currentPos.y > maxDrag || startPos.y - currentPos.y < -maxDrag))
                    {
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
                    GetComponentInParent<playerBehavior>().updateMana(-spell.GetComponent<spellBehavior>().cost);
                    GetComponentInParent<playerBehavior>().cast.Play();
                    GetComponentInParent<playerBehavior>().animations.SetTrigger("Attack");
                    Vector2 mousePos = Mouse.current.position.value;
                    Vector2 endPos = cam.ScreenToWorldPoint(mousePos);
                    Vector2 force = (startPos - endPos) * multiplier;

                    spell.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                    spell.GetComponent<Collider2D>().enabled = true;
                    spell.GetComponent<Animator>().enabled = true;
                    spell.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                    oldSpell = spell;
                    spell = null;
                }
            }
        }
    }
    
    public void updateSpell(int newSpell)
    {
        currentSpell= newSpell;
        if(spell !=null){
            Destroy(spell);
        }
        spell = Instantiate(spells[newSpell],transform.position,Quaternion.identity);
    }
}
