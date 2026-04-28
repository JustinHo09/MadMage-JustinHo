using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class LauncherBehavior : MonoBehaviour
{
    public float gravityScale;
    public bool isCasting;
    
    public GameObject spell;
    private GameObject oldSpell;
    public GameObject[] spells;
    public int currentSpell;
    private int currentMana;

    public Sprite[] sprites;
    public GameObject spellPrev;

    public int lowestCost;
    
    private Camera cam;
    public float multiplier;
    public float maxDrag;
    private Vector2 startPos;
    
    public int dots;
    private LineRenderer tracer;
    public float timeInterval;
    
    public GameObject warningSign;
    public TMP_Text castable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tracer = GetComponent<LineRenderer>();
        tracer.enabled = false;
        isCasting = false;
        currentSpell=0;
        cam = Camera.main;
        spellPrev.GetComponent<SpriteRenderer>().sprite = sprites[0];
        spell = Instantiate(spells[currentSpell],transform.position,Quaternion.identity);
        lowestCost = spells[0].GetComponent<spellBehavior>().cost;
        // Find the lowest cost spell to make sure the game is still beatable
        for (int i = 1; i < spells.Length; i++)
        {
            if (spells[i].GetComponent<spellBehavior>().cost < lowestCost)
            {
                lowestCost = spells[i].GetComponent<spellBehavior>().cost;
            }
        }
    }

    void Update()
    {
        // Get the current mana 
        currentMana = GetComponentInParent<playerBehavior>().currentMana;
        
        // If the currentmana - spell is >= 0 and thus valid then disblae the warning sign otherwise enable it
        if(currentMana - spells[currentSpell].GetComponent<spellBehavior>().cost >= 0 || currentMana == 0)
        {
            warningSign.SetActive(false);
        }else{
            warningSign.SetActive(true);
        }

                
        // only create the spell if there is none and there is enough mana to cast it
        if (spell == null && oldSpell == null && currentMana - lowestCost >= 0)  
        {
            spell = Instantiate(spells[currentSpell],transform.position,Quaternion.identity);
        }
        
        // IF you are not activly dragging your mouse and have enught mana to cast any spell
        if (isCasting == false && currentMana - lowestCost >= 0)
        {
            // left arrow and chose the spells that are in the left of spell selector, so th previous spell
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
            {
                currentSpell--;
                if (currentSpell < 0)
                {
                    currentSpell = spells.Length-1;
                }
                updateSpell(currentSpell);
                spellPrev.GetComponent<SpriteRenderer>().sprite = sprites[currentSpell];
            }
                    
            //right arrow and chose the spells that are in the right of spell selector, so the next spell
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
            {
                currentSpell++;
                currentSpell = currentSpell % spells.Length;
                updateSpell(currentSpell);
                spellPrev.GetComponent<SpriteRenderer>().sprite = sprites[currentSpell];
            }
        }
                
        // If the player is activly holding a spell then start launchh onclick
        if(spell!=null){ 
            // if they ahve enouoght mana to even cast this spell then allow launch otherwise not
            if (currentMana - spell.GetComponent<spellBehavior>().cost >= 0)
            {
                // Mouse click down so it is starting pos and keep track of it
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    isCasting = true;
                    Vector2 mousePos = Mouse.current.position.value;
                    startPos = cam.ScreenToWorldPoint(mousePos);
                }

                // Mouse drag to get every current position, and update the tracer, and force at each update
                if (Mouse.current.leftButton.isPressed)
                {
                    Vector3 mousePos = Mouse.current.position.value;
                    Vector2 currentPos = cam.ScreenToWorldPoint(mousePos);
                    Vector2 dragDist = startPos - currentPos;
                    // Will ensure that the dragDistance will never excede the maximum
                    dragDist = Vector2.ClampMagnitude(dragDist, maxDrag);
                    
                    showTracer(dragDist * multiplier*Time.fixedDeltaTime);
                }

                // Mouse release to get final position and luanch the spell
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    // Update teh player's mana by teh spell cost, start it's play animation, and play cast audio
                    GetComponentInParent<playerBehavior>().updateMana(-spell.GetComponent<spellBehavior>().cost);
                    GetComponentInParent<playerBehavior>().cast.Play();
                    GetComponentInParent<playerBehavior>().animations.SetTrigger("Attack");
                    
                    // Update the mouses position to be where it is at now and find its actual coordinates
                    Vector3 mousePos = Mouse.current.position.value;
                    Vector2 endPos = cam.ScreenToWorldPoint(mousePos);
                    // Find the distance it is dragged and make sure it does not exccede the max range
                    Vector2 dragDist = startPos - endPos;
                    dragDist = Vector2.ClampMagnitude(dragDist, maxDrag);
                    // calculate the force by multiplying the distance by a mmultiplier
                    Vector2 force = dragDist * multiplier;
                
                    // Give the spell the gravityy we want, and enable its collider and animator
                    spell.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                    spell.GetComponent<Collider2D>().enabled = true;
                    spell.GetComponent<Animator>().enabled = true;
                    
                    // Add force to give it the launnch trajcetory
                    spell.GetComponent<Rigidbody2D>().AddForce(force * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    
                    // Keep track of old spell by making it the one that was just launched
                    // then set the new spell to null and disbale the launcher
                    oldSpell = spell;
                    spell = null;
                    tracer.enabled = false;
                }
            }
        }
    }
    
    // Update is called once per frame
    
    // Update what spell the player is currently holding
    public void updateSpell(int newSpell)
    {
        // If the spell is not null destory it then make a new one that is the one given
        currentSpell= newSpell;
        if(spell !=null){
            Destroy(spell);
        }
        spell = Instantiate(spells[newSpell],transform.position,Quaternion.identity);
    }

    // This sill enable the tracer, and give it positions equal to how many dots we want for it
    public void showTracer(Vector2 force)
    {
        tracer.enabled = true;
        tracer.positionCount = dots;

        // Make an array of vectors to store teh locationi of where we want those dots to be
        Vector3[] points = new Vector3 [dots];
        
        // Calculate veloctiy manually by difiving inputed ofrce by mass
        Vector2 velocity = force / spell.GetComponent<Rigidbody2D>().mass;
        // get the starting positino of the spell which is the launcher's position
        Vector2 startPos = transform.position;

        // Put each dot at the position the projectile will be at after a certain time
        //using the kinemaitc equation for position
        for (int i = 0; i < dots; i++)
        {
            float t = i * timeInterval;
            Vector2 pos = startPos + velocity * t + 0.5f * (Physics2D.gravity*gravityScale)* t * t;
            points[i] = pos;
        }
        // Give the pracer those paoints and it will draw them
        tracer.SetPositions(points);
    }
}
