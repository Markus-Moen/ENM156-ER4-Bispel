using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeToBe : MonoBehaviour
{
    private Rigidbody2D bee;
    private int rightEdgeX = 3; //The edges of the screen.
    private int leftEdgeX = -3;
    private int topEdgeY = 5;
    private int downEdgeY = -5;
    private float width;
    private float height;
    private bool buzzing = false; //If buzzing, the bee will move in a circle around current position
    private float buzzRounds = 0;
    private Vector2 beeSpeed = new Vector2(1f, 1f);
    private Vector2 currentPos;
    private float updates = 0;
    private float maxBuzzSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        bee = GetComponent<Rigidbody2D>();
        bee.velocity = beeSpeed; //Set an inital speed
        
        var renderer = bee.GetComponent<Renderer>(); //Get the dimensions of the object.
        width = renderer.bounds.size.x;
        height = renderer.bounds.size.y; 
    }

    // Update is called once per frame
    void Update()
    {
        
      if(!buzzing || 100000 <= buzzRounds) {
          buzzRounds = Random.Range(1, 100001); //Random 1-100 000 (integer)
      }      
      if(buzzRounds <= 500) {
        Debug.Log("Buzzing");
        if(updates == 0) {
            float xSpeed = Random.Range(-maxBuzzSpeed, maxBuzzSpeed); //-maxBuzzSpeed <= speed <= maxBuzzSpeed (float)
            float ySpeed = Random.Range(-maxBuzzSpeed, maxBuzzSpeed);
            
            //Set speed if close to edge
            if(bee.position.x <= leftEdgeX + width)
                xSpeed = maxBuzzSpeed;
            else if(rightEdgeX - width <= bee.position.x)
                xSpeed = -maxBuzzSpeed;
            else if(bee.position.y <= downEdgeY + height)
                ySpeed = maxBuzzSpeed;
            else if(topEdgeY - height <= bee.position.y)
                ySpeed = -maxBuzzSpeed;
            
            //Check if new direction x-axis, if so flip the bee.
            if(Mathf.Sign(bee.velocity.x) != Mathf.Sign(xSpeed)) {
                var sprite = bee.GetComponent<SpriteRenderer>();
                sprite.flipX = !sprite.flipX;
            }                          
            bee.velocity = new Vector2(xSpeed, ySpeed);
        }
        else { //Go in the same direction a little while
            if(updates >= 100)
                updates = 0; //Change direction
            else
                updates = updates + 0.01f;
        }
        buzzRounds--;
        if(buzzRounds == 0) { //Stop buzzing?
            buzzing = false;
            buzzRounds = 5000; //Go straight for a bit
        }           
      }
      else {    
            buzzRounds = buzzRounds + 0.1f;
            bee.velocity = beeSpeed; //Change to inital speed in current direction
        }
    
    //Turn at edges and flip picture if necessary.
    if(bee.position.x <= leftEdgeX + width/2 || rightEdgeX - width/2 <= bee.position.x) {              
        bee.velocity = new Vector2(-bee.velocity[0], bee.velocity[1]);
        var sprite = bee.GetComponent<SpriteRenderer>();
        sprite.flipX = !sprite.flipX;
    }
    if(bee.position.y <= downEdgeY + height)
        bee.velocity = new Vector2(bee.velocity[0], Mathf.Abs(bee.velocity[1]));
    else if(topEdgeY - height <= bee.position.y)
        bee.velocity = new Vector2(bee.velocity[0], -Mathf.Abs(bee.velocity[1]));
    
    //Change saved velocity if direction changed at an edge.
    beeSpeed = bee.velocity;     
    }
}
