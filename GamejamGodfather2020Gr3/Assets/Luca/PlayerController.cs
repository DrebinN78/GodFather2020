using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float moveInput;


    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool isJumping;


    [SerializeField] private int playerID = 0;

    [SerializeField] private Player playerC;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerC = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {
        //Move
        moveInput = playerC.GetAxis("MoveH");

            if(moveInput < 0){
             rb.velocity = new Vector2(-speed, rb.velocity.y);
             //transform.eulerAngles = new Vector3(0, 180, 0);
        } else if(moveInput > 0){
             rb.velocity = new Vector2(speed, rb.velocity.y);
             //transform.eulerAngles = new Vector3(0, 0, 0);
        } else if(moveInput == 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Jump

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded){
        if(playerC.GetButtonDown("Jump")){
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        }
    }
}
