using UnityEngine;

public class spellBehavior : MonoBehaviour
{
    public ParticleSystem burst;
    public int damage;
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
        if (collision.gameObject.tag.Equals("Block"))
        {
            //Destroy(this);
        }
        //destory and do damage to blocks and enemies
        
    }
}
