using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    public float jumpHeight;
    public float speed;
    private Rigidbody2D rigidbody2d;
    enum States
    {
        Walking,
        Jumping,        
    }
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {  
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += movement * Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpHeight;
        }




    }
    
}
