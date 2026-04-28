using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float hp;
    public int points;
    
    private bool ailment;
    private float dotDMG;

    private float start;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start = 0.0f;
        ailment = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ailment)
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
        }
        
        // if hp <= 0 then its dead, play the death audio, update the player's score, and destory it
        if(hp <=0 ) {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().death.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehavior>().updateScore(points);
            Destroy(gameObject,0.5f);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag.Equals("Spell")){
            // decrease it's hp
            hp = hp - collision.gameObject.GetComponent<spellBehavior>().damage;
            // if it is a dot spell then makit it start taking dot damage
            if (collision.gameObject.GetComponent<spellBehavior>().dot)
            {
                ailment = true;
                start = Time.time;
                dotDMG = collision.gameObject.GetComponent<spellBehavior>().dotDMG;
            }
        }
    }
}
