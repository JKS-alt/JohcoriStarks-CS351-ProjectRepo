using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerZoneScript : MonoBehaviour
{
    // Assign these in the Inspector
    public TMP_Text output;
    public string textToDisplay;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       if (collision.gameObject.tag == "Player")
        {
            
            output.text = textToDisplay;
        }
       Debug.Log("Trigger entered by: " + collision.gameObject.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
