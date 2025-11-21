using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
      public Rigidbody2D playerRb;
    public float speed; 
    public float input; 
    
  
    // Update is called once per frame
    void Update()
    {
       input = Input.GetAxisRaw("Horizontal"); 
    }

    void FixedUpdate()
    {
        playerRb.linearVelocity = new Vector2 (input * speed, playerRb.linearVelocity.y);

    }
}
