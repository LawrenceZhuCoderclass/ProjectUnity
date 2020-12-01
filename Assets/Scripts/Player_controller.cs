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
    public enum CharacterState
    { 
        Idle,
        Jumping,
        Walking,
        InAir
    }
    public CharacterState characterState;

    void Awake()
    {
        characterState = CharacterState.Idle;
    }
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Sprite = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AnimatorGroundCheck();
        switch (characterState)
            {
                case CharacterState.Idle:
                    Idle();
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        characterState = CharacterState.Walking;
                    }
                    if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                    {
                        characterState = CharacterState.Jumping;
                    }
                    break;
                case CharacterState.Jumping:
                    Jumping();
                    characterState = CharacterState.Idle;
                    break;
                case CharacterState.Walking:
                    Walking();
                    if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                        {
                            characterState = CharacterState.Jumping;
                        }
                    break; 
            }

        Debug.Log(characterState);
    }

    void AnimatorGroundCheck()
    {
        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
        else if (IsGrounded() == false)
        {
            animator.SetBool("IsJumping", true);
        }


    }
    void Idle()
    {

    }
    void Jumping()
    {
        animator.SetBool("IsJumping", true);
        rigidbody2d.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }
    void Walking()
    {
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move, 0f, 0f);
        if (move < 0 && Sprite != null)
        {
            Sprite.flipX = true;
        }
        if (move > 0)
        {
            Sprite.flipX = false;
        }
        transform.position += movement * Time.deltaTime * speed;
        animator.SetFloat("Speed", Mathf.Abs(move));
    }
    private bool IsGrounded()
    {
        float Height = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, Height, groundLayerMask);
        
        return raycastHit.collider != null;
        
    }


}

