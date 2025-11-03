using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Enemy : MonoBehaviour
{//enemy health
    public int health;

    //prefab to spawn when emeby die
    public GameObject DeathAffect;

    public void TakeDamage(int damage)
    {
        health -= damage;
      
        if (health <= 0)
        {
            Die();
        }
    }
       
    void Die()
    {
       // Instantiate(DeathAffect, transform.position, Quaternion, identity);
            
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
