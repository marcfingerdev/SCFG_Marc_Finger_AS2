using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodParticle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Finish")
        {
            //Debug.Log("Hit Dead");
            Destroy(this.gameObject);
        }
    }
}
