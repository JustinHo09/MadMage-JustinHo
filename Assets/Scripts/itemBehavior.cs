using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class itemBehavior : MonoBehaviour
{
    public bool buff;
    private int manaChange;

    public ParticleSystem burst;
    public int playerMana;

    public AudioSource destroy;
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMana = player.GetComponent<playerBehavior>().currentMana;
        int rng = Random.Range(0, 50);
        if (rng > 25)
        {
            manaChange = 25;
        }
        else
        {
            manaChange = playerMana/10 ;
        }
        if (!buff)
        {
            manaChange = -manaChange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<playerBehavior>().updateMana(manaChange);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        burst.Play();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        destroy.Play();
        Destroy(gameObject);
    }
}
