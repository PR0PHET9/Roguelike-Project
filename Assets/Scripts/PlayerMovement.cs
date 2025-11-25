using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
       private Rigidbody2D rb;
      private GameObject Rogue;
      private GameObject Collector;
      //public Animator animator;
      public float moveSpeed = 5f;
    float horizontalMove = 0f;
 
        void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  
    // Update is called once per frame
    void Update()
  {

  }

        void FixedUpdate() // Use FixedUpdate for physics-related movement
    {
        float horizontalMove = Input.GetAxis("Horizontal"); // Get input value (-1 to 1)
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Calculate target velocity
        Vector2 targetVelocity = new Vector2(horizontalMove * moveSpeed, rb.linearVelocity.y);

        // Apply velocity to the Rigidbody2D
        rb.linearVelocity = targetVelocity;
    }
}
