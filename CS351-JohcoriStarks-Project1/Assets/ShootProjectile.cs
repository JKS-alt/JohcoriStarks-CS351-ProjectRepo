using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform Firepoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
            

        }


    

    void Shoot ()
    {
        Instantiate(ProjectilePrefab, Firepoint.position, Firepoint.rotation);
    }
}
