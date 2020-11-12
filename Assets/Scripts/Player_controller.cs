using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    [SerializeField] public LayerMask groundLayerMask;
    public float jumpHeight;
    public float speed;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer Sprite;
    public Animator animator;
    enum States
    {
        Walking,
        Jumping,
    }
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Sprite = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    void Update()
    {  
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move, 0f, 0f);
        if (move<0&& Sprite != null){
                Sprite.flipX = true;
        }
        if (move >0)
        {
            Sprite.flipX = false;
        }
        transform.position += movement  * Time.deltaTime* speed;
        animator.SetFloat("Speed", Mathf.Abs(move));
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
            rigidbody2d.AddForce (transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
    }


    private bool IsGrounded()
    {
        float Height = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, Height, groundLayerMask);
        
        return raycastHit.collider != null;
        
    }


}

