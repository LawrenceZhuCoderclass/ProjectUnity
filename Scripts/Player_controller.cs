using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class Player_controller : MonoBehaviour
{
    [SerializeField] public LayerMask groundLayerMask;

    public float jumpHeight;
    public float speed;

    public Material Diffuse;
    public Material DefaultMaterial;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2D;

    private TilemapRenderer GroundMaterial;
    private TilemapRenderer PlatformMaterial;
    private TilemapRenderer WallMaterial;
    private TilemapRenderer DecorMaterial;

    private SpriteRenderer Sprite;
    public Animator animator;

    public GameObject GroundFloor;
    public GameObject Platforms;
    public GameObject Walls;
    public GameObject Decor;

    public TextMeshProUGUI ScoreText;

    private int count;
    public int TotalDiamonds;

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
        Wall,
        Decor
    }


    void Awake()
    {
        characterState = CharacterState.Idle;
        visionState = VisionState.Standard;
    }
    void Start()
    {
        ScoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();

        GroundFloor = GameObject.FindWithTag("Ground");
        Platforms = GameObject.FindWithTag("Platforms");
        Walls = GameObject.FindWithTag("Wall");
        Decor = GameObject.FindWithTag("Decor");

        GroundMaterial = GroundFloor.GetComponent<TilemapRenderer>();
        PlatformMaterial = Platforms.GetComponent<TilemapRenderer>();
        WallMaterial = Walls.GetComponent<TilemapRenderer>();
        DecorMaterial = Decor.GetComponent<TilemapRenderer>();

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Sprite = transform.GetComponent<SpriteRenderer>();

        SetCountText();
    }
    
    void FixedUpdate()
    {
        AnimatorGroundCheck();

        switch (characterState)
        {
            case CharacterState.Idle:
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
                if (Input.GetKeyDown(KeyCode.J))
                {
                    visionState = VisionState.Decor;
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
            case VisionState.Decor:
                DecorVision();
                if (Input.GetKeyDown(KeyCode.J))
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
            PlatformMaterial.material = Diffuse;
            GroundMaterial.material = Diffuse;
            WallMaterial.material = Diffuse;
            DecorMaterial.material = Diffuse;
        }

        void PlatformVision()
        {
        
            PlatformMaterial.material = DefaultMaterial;
        }
        void GroundVision()
        {
            
            GroundMaterial.material = DefaultMaterial;
        }
        void WallVision()
        {
            WallMaterial.material = DefaultMaterial; 
        }
        void DecorVision()
        {
            DecorMaterial.material = DefaultMaterial;
        }

    ///-------------------------Een systeem waar de player de diamanten kan oppakkken------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Diamond"))
        {       
            Destroy(other.gameObject);
            count = count + 1;
            ScoreText.text = count.ToString() + "/" + TotalDiamonds;

        }
    }
    ///--------------------------Een systeem dat de score laar zien-------------------------
    void SetCountText()
    {
        ScoreText.text = count.ToString() + "/" + TotalDiamonds;
    }

            //--------------- Ground Check---------------------------------------------------
    private bool IsGrounded()
        {
            float Height = 0.1f;
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, Height, groundLayerMask);
            return raycastHit.collider != null;
        }    
        
}

