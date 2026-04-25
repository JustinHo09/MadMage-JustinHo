using UnityEngine;

public class blockBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float hp;
    public int points;

    public bool ailment;
    public float dotDMG;
    private int counter;

    private float start;
    void Start()
    {
        start = 0.0f;
        ailment = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ailment && Time.time - start < 4.0f )
        {
                hp -= dotDMG;
               
        }else
        {
            ailment = false;
        }
        
        if(hp <=0 ){

            GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehavior>().updateScore(points);
            Destroy(gameObject,0.1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag.Equals("Spell")){
            hp = hp - collision.gameObject.GetComponent<spellBehavior>().damage;
            if (collision.gameObject.GetComponent<spellBehavior>().dot == true)
            {
                dotDMG = collision.gameObject.GetComponent<spellBehavior>().dotDMG;
                start = Time.time;
                ailment = true;
            }
        }
    }
}
