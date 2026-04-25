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
        player = GameObject.FindGameObjectWithTag("Player");
        playerMana = player.GetComponent<playerBehavior>().maxMana;
        int rng = Random.Range(0, 50);
        if (rng > 25)
        {
            manaChange = 25;
        }
        else
        {
            manaChange = playerMana/10 ;
        }

        if (random)
        {
            int chance = Random.Range(0, 11);
            if (chance <= 5)
            {
                buff = false;
            }
        }

        if (!buff)
        {
            manaChange = -manaChange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (random && Time.time - currentTime > 5.0f)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            currentTime = Time.time;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spell"))
        { 
            player.GetComponent<playerBehavior>().updateMana(manaChange);
            //destroy.Play();
            Destroy(gameObject, 0.01f);
        }
    }

    
}
