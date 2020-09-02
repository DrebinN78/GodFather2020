using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float moveInput;
    public float moveInput2;


    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public bool canDash;
    public bool isDashing;
    public int direction;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

    public GameObject arrow;

    public float dashCoolDown;
    public float startDashCoolDown;
    public bool dashGroundReady;

    [SerializeField] private int playerID = 0;
}
