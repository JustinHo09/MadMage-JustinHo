using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float hp;
    public int points;

    public ParticleSystem deathVFX;
    
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
            if (Time.deltaTime - start > 1.0f)
            {
                hp -= dotDMG;
            }

            if (start >= 5.0)
            {
                ailment = false;
                start = 0.0f;
            }
        }
        
        if(hp <=0 ) {
            //deathVFX.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehavior>().updateScore(points);
            Destroy(gameObject,0.5f);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag.Equals("Spell")){
            hp = hp - collision.gameObject.GetComponent<spellBehavior>().damage;
            if (collision.gameObject.GetComponent<spellBehavior>().dot)
            {
                ailment = true;
                dotDMG = collision.gameObject.GetComponent<spellBehavior>().dotDMG;
            }
        }
    }
}
