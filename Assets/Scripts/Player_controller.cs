using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_controller : MonoBehaviour
{
    [SerializeField] public LayerMask groundLayerMask;
    [SerializeField] public LayerMask PlatformLayerMask;
    public float jumpHeight;
    public float speed;

    public Material Diffuse;
    public Material DefaultMaterial;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2D;

    private SpriteRenderer Sprite;
    public Animator animator;

    public TilemapRenderer GroundFloor;
    public TilemapRenderer Platforms;
    public TilemapRenderer Walls;

    public CharacterState characterState;
    public VisionState visionState;

    public enum CharacterState
    { 
        Idle,
        Jumping,
        Walking,
        InAir
    }
    public enum VisionState
    {
        Standard,
        Ground,
        Platform,
        Wall
    }


    void Awake()
    {
        characterState = CharacterState.Idle;
        visionState = VisionState.Standard;
    }
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Sprite = transform.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
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
                break;
            case CharacterState.Walking:
                Walking();
                break;
        }
    }
    void Update()
    {
        AnimatorGroundCheck();
        switch (characterState)
        {
            case CharacterState.Idle:
                Idle();
                if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                {
                    characterState = CharacterState.Jumping;
                }
                break;
            case CharacterState.Walking:
                if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                {
                    characterState = CharacterState.Jumping;
                }
                break;
            case CharacterState.Jumping:
                Jumping();
                characterState = CharacterState.Idle;
                break;
        }
        switch (visionState)
        {
            case VisionState.Standard:
                Standard();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    visionState = VisionState.Ground;

                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    visionState = VisionState.Platform;
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    visionState = VisionState.Wall;
                }
                break;
            case VisionState.Ground:
                GroundVision();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    visionState = VisionState.Standard;
                }
                break;
            case VisionState.Platform:
                PlatformVision();
                if (Input.GetKeyDown(KeyCode.G))
                {
                    visionState = VisionState.Standard;
                }
                break;
            case VisionState.Wall:
                WallVision();
                if (Input.GetKeyDown(KeyCode.H))
                {
                    visionState = VisionState.Standard;
                }
                break;
        }
    }

        //--------------------------------Ground Check voor Animator-------------------------------
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
        ///------------------------------- Character States--------------------------------------
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
        ///-----------------------------------------VisionStates-------------------------------------
        void Standard()
        {
            //Debug.Log("Log");
            Platforms.material = Diffuse;
            GroundFloor.material = Diffuse;
            Walls.material = Diffuse;
        }

        void PlatformVision()
        {
            //Debug.Log("eeeee");
            Platforms.material = DefaultMaterial;
        }
        void GroundVision()
        {
            //Debug.Log("AA");
            GroundFloor.material = DefaultMaterial;
        }
        void WallVision()
        {
            Walls.material = DefaultMaterial; 
        }
        //--------------- Ground Check---------------------------------------------------
        private bool IsGrounded()
        {
            float Height = 0.1f;
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, Height, groundLayerMask);
            return raycastHit.collider != null;
        }    
}

