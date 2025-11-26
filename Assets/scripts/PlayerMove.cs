using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal"); // "Horizontal" comes from input manager
        float vertical = Input.GetAxis("Vertical");

        //same as this.gameObject.transform. arguments are x, y, and z
        this.gameObject.transform.Translate(horizontal * moveSpeed * Time.deltaTime,
            vertical * moveSpeed * 2 * Time.deltaTime
            , 0); // delta time of update method called last time

        
        bool fire1 = Input.GetButtonDown("Fire1");
        if (fire1)
        {
            UnityEngine.Debug.Log("Fire pressed");
        }

        //animator.SetBool("isWalkingUp", vertical > 0);
        animator.SetBool("isRunningRight", Mathf.Abs(horizontal) > 0.01f);
        //animator.SetBool("isWalkingLeft", horizontal < 0);

        if (horizontal > 0.01f)
        {
            spriteRenderer.flipX = false;   // right
        }
        else if (horizontal < -0.01f)
        {
            spriteRenderer.flipX = true;    // left
        }

        animator.SetBool("isJumping", !isGrounded);


    }
}