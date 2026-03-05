using System;
using UnityEngine;

public class itemBehavior : MonoBehaviour
{
    public bool buff;
    private int manaChange;

    public ParticleSystem burst;

    public AudioSource destroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (buff)
        {
            manaChange = 20;
        }else{
            manaChange = -20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<playerBehavior>().currentMana += manaChange;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        burst.Play();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        destroy.Play();
        Destroy(this);
    }
}
