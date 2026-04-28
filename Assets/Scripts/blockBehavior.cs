using UnityEngine;

public class blockBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Genderal block info 
    public float hp;
    public int points;
    public int blockType;
    
    // Used for dot
    public bool ailment;
    public float dotDMG;
    
    // Keep track of ailment start time
    private float start;
    void Start()
    {
        start = 0.0f;
        ailment = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If under ailment and the time elapsed is < 4 seconds then take teh DOT damage
        if (ailment && Time.time - start < 4.0f )
        {
                hp -= dotDMG;
               
        }else
            // If four seconds have passed then disable ailment to stop the dot damage
        {
            ailment = false;
        }
        
        if(hp <=0 ){
            // When teh block is destroyed increase the player's points, play the destory audio, and destroy it
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehavior>().updateScore(points);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().breaks[blockType].Play();
            Destroy(gameObject,0.1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag.Equals("Spell")){
            // Make the block take damage
            hp = hp - collision.gameObject.GetComponent<spellBehavior>().damage;
            // If it's a dot spell, make it start taking dot damage
            if (collision.gameObject.GetComponent<spellBehavior>().dot == true)
            {
                dotDMG = collision.gameObject.GetComponent<spellBehavior>().dotDMG;
                start = Time.time;
                ailment = true;
            }
        }
    }
}
