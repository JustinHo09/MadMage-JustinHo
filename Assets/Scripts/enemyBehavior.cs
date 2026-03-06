using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public int hp;
    public int points;

    public ParticleSystem deathVFX;
    public AudioSource death;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag.Equals("Spell")){
            hp = hp - collision.gameObject.GetComponent<spellBehavior>().damage;
            if(hp <=0 )
            {
                deathVFX.Play();
                death.Play();
                GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehavior>().updateScore(points);
                Destroy(gameObject,1.0f);
            }
        }
    }
}
