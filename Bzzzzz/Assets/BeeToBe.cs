using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeToBe : MonoBehaviour
{
    private Rigidbody2D bee;
    private int rightEdgeX = 5; //The edges of the screen. Temporary values.
    private int leftEdgeX = -5;
    private int topEdgeY = 5;
    private int downEdgeY = -5;
    private float width;
    private float height;
    private bool buzzing = false; //If buzzing, the bee will move in a circle around current position
    private float buzzRounds = 0;
    private float rotateSpeed = 2f;
    private float buzzRadius = 1f;
    private Vector2 beeSpeed = new Vector2(2f, 1f);
    private float angle;
    private Vector2 center;
    private Vector2 currentPos;
    private int updates = 0;
    
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
        //Randomly change directions with low probability to "buzz"
        if(!buzzing || 5001 <= buzzRounds) {
            buzzRounds = Random.Range(1, 5001); //Number between 1-5000.
            if(beeSpeed[1] < 0)
                center = new Vector2(bee.position[0], bee.position[1] - buzzRadius); //Choose where the circle to buzz is.
            else
                center = new Vector2(bee.position[0], bee.position[1] + buzzRadius); //Choose direction to buzz. These two may be changed, little jumpy now.
            if(beeSpeed[0] < 0)
                rotateSpeed = -rotateSpeed;
            else
                rotateSpeed = Mathf.Abs(rotateSpeed);
        }
        if(buzzRounds <= 10) { //probability to buzz 10/5000
            if(bee.velocity[0] != 0) {                
                beeSpeed = bee.velocity; //Save to restart.
                currentPos = bee.position;
            }
            bee.velocity = new Vector2(0f, 0f);
            buzzing = true;
            //Turn in a circle
            angle += rotateSpeed * Time.deltaTime;
            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * buzzRadius;
            var newPosition = center + offset;                          
            
            //Check so that new position is not outside of screen.
            if(newPosition[0] <= leftEdgeX + width/2)
                bee.position = new Vector2(leftEdgeX + width/2, newPosition[1]);
            else if(rightEdgeX - width/2 <= newPosition[0])
                bee.position = new Vector2(rightEdgeX - width/2, newPosition[1]);
            else if(newPosition[1] <= downEdgeY + height/2)
                bee.position = new Vector2(newPosition[0], downEdgeY + height/2);
            else if(topEdgeY - height/2 <= newPosition[1])
                bee.position = new Vector2(newPosition[0], topEdgeY - height/2);
            else
                bee.position = newPosition; 
            
            updates += 1;
            if(updates >= 350){ //One circle
                buzzing = false;
                buzzRounds = 4000; //Go straight a little after a circle.
                updates = 0;
            }
        }
        else {    
            buzzing = false;
            bee.velocity = beeSpeed;
            //Check if you do hit edge.
            if(bee.position.x <= leftEdgeX + width/2 || rightEdgeX - width/2 <= bee.position.x) {              
                bee.velocity = new Vector2(-bee.velocity[0], bee.velocity[1]);
                var sprite = bee.GetComponent<SpriteRenderer>();
                sprite.flipX = !sprite.flipX;
            }
            if(bee.position.y <= downEdgeY + height/2 || topEdgeY - height/2 <= bee.position.y)
                bee.velocity = new Vector2(bee.velocity[0], -bee.velocity[1]);
            buzzRounds += 1;
            //Change saved velocity if direction changed at an edge.
            beeSpeed = bee.velocity;
        }
    }
}
