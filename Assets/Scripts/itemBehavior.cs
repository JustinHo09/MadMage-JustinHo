using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class itemBehavior : MonoBehaviour
{
    public bool buff;
    public bool random;
    private int manaChange;

    public Sprite[] sprites;
    private float currentTime;

    public int playerMana;
    

    public AudioSource destroy;
    GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Keep track of the player's max mana
        player = GameObject.FindGameObjectWithTag("Player");
        playerMana = player.GetComponent<playerBehavior>().maxMana;
        
        // Determine how  much the item will change the mana by, a flat 25 or 10% of max
        int rng = Random.Range(0, 50);
        if (rng > 25)
        {
            manaChange = 25;
        }
        else
        {
            manaChange = playerMana/10 ;
        }

        // If it is a random potion than randomly decide if it will be a buff or not
        if (random)
        {
            int chance = Random.Range(0, 11);
            if (chance <= 5)
            {
                buff = false;
            }
        }

        // If its not a buff then make it decrease the mana
        if (!buff)
        {
            manaChange = -manaChange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If it is the random one, than have it randomly change sprites every 2.5 second
        if (random && Time.time - currentTime > 2.5f)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            currentTime = Time.time;
        }
    }
    
    // Item is a trigger so that it doesn't affect spell trajectory
    void OnTriggerEnter2D(Collider2D collision)
    {
        // It it gets hit by a spell then update the player's mana accordingly,
        // play destory audio, and destory the potion.
        if (collision.CompareTag("Spell"))
        { 
            player.GetComponent<playerBehavior>().updateMana(manaChange);
            destroy.Play();
            Destroy(gameObject, 0.01f);
        }
    }

    
}
