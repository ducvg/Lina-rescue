using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private int moveAnimation = Animator.StringToHash("isMove");

    public bool isFacingRight;
    private Vector2 moveInput;
    private bool isJumping, isJumpCut;

    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        moveAction = InputManager.instance.playerInputs.Player.Move;
        jumpAction = InputManager.instance.playerInputs.Player.Jump;
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.IsPressed() && IsGrounded())
        {
            isJumping = true;
        }
        if (jumpAction.WasReleasedThisFrame())
        {
            isJumpCut = true;
        }
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return hit;
    }

    private void FixedUpdate()
    {
        animator.SetBool(moveAnimation, MoveControl());
        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            isJumping = false;
        }
        if (isJumpCut && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);
            isJumpCut = false;
        }
    }

    private bool MoveControl()
    {
        rb.linearVelocityX = moveInput.normalized.x * speed;
        if (moveInput.x > 0 && !isFacingRight)
            Flip();
        else if (moveInput.x < 0 && isFacingRight)
            Flip();
        return Mathf.Abs(moveInput.x) > 0.1f;
        
        // rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocityY);

        // if (moveInput > 0 && !isFacingRight)
        //     Flip();
        // else if (moveInput < 0 && isFacingRight)
        //     Flip();

        // return Mathf.Abs(moveInput) > 0.1f;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}