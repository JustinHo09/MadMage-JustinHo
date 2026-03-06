using UnityEngine;

public class blockBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public AudioSource destroy;
    public ParticleSystem destroyVFX;
    
    public int hp;
    public int points;
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
            if(hp <=0 ){
                //destroyVFX.Play();
                //destroy.Play();
                GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehavior>().updateScore(points);
                Destroy(gameObject,0.4f);
            }
        }
    }
}
