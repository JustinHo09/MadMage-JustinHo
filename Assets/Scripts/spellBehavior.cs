using UnityEngine;

public class spellBehavior : MonoBehaviour
{
    public ParticleSystem burst;
    public int damage;
    public AudioSource destroy;

    public int cost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D collision) {
        burst.Play();
        //destroy.Play();
        Destroy(gameObject,0.4f);
    }
}
