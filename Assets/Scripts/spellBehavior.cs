using UnityEngine;

public class spellBehavior : MonoBehaviour
{
    public int damage;
    public AudioSource destroy;

    public int cost;
    
    public Animator animations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D collision) {
        animations.SetTrigger("Collision");
        //destroy.Play();
        Destroy(gameObject,0.5f);
    }
}
