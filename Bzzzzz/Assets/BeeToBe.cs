using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeToBe : MonoBehaviour
{
    private Rigidbody2D bee;
    private int rightEdgeX = 5; //The edges of the screen. Temporary values.
    private int leftEdgeX = -5;
    private int rightEdgeY = 3;
    private int leftEdgeY = -3;
    private float width;
    private float height;
    
    // Start is called before the first frame update
    void Start()
    {
        bee = GetComponent<Rigidbody2D>();
        bee.velocity = new Vector2(3f, 2f); //Set an inital speed
        
        var renderer = bee.GetComponent<Renderer>(); //Get the dimensions of the object.
        width = renderer.bounds.size.x;
        height = renderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Chech if you hit the an edge and reverse direction if you did. 
        if(bee.position.x <= leftEdgeX + width/2 || rightEdgeX <= bee.position.x - width/2) {
            bee.velocity = new Vector2(-bee.velocity[0], bee.velocity[1]);
        }
        if(bee.position.y <= leftEdgeY || rightEdgeY + height/2 <= bee.position.y - height/2) {
            bee.velocity = new Vector2(bee.velocity[0], -bee.velocity[1]);
        }
    }
}
