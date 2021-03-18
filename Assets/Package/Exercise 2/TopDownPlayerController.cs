using System;
using System.Collections;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 5f;

    private float currentSpeed;
    private Vector2 moveVector;

    private bool isMoving = false;

    [Header("Attack")]
    [SerializeField] bool attack = false;
    [SerializeField] float attackTime = 0.1f;
    [SerializeField] float attackingTimer = -1f;

    // Components
    private Rigidbody2D rb2d;
    private Animator anim;
    private AudioSource audioS;


    // Awake
    private void Awake()
    {
        // Init
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();

        // Variables
        currentSpeed = maxSpeed;
    }

    // Update
    private void Update()
    {
        GetInput();
        ProcessInput();
        CountdownAttack();
    }

    // Fixed Update
    private void FixedUpdate()
    {
        // Bewegen
        if (isMoving)
            rb2d.MovePosition(rb2d.position + moveVector.normalized * currentSpeed * Time.deltaTime);
        else
            rb2d.velocity = Vector2.zero;
    }

    // ========== Input ==========
    // Get Input
    private void GetInput()
    {
        // Movement
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Action
        attack = Input.GetButtonDown("Jump");
    }

    // Process Input
    private void ProcessInput()
    {
        // Movement
        isMoving = (Mathf.Abs(moveVector.x) > Mathf.Epsilon || Mathf.Abs(moveVector.y) > Mathf.Epsilon);

        if (isMoving)
        {
            // Animator einstellen
            anim.SetFloat("input_x", moveVector.x);
            anim.SetFloat("input_y", moveVector.y);
        }

        anim.SetBool("isMoving", isMoving);

        // Attack
        if (attack && attackingTimer <= 0f)
        {
            anim.SetBool("isAttacking", true);
            currentSpeed = 0;

            audioS.PlayOneShot(audioS.clip);

            attackingTimer = attackTime;
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    // ========== Attack ==========
    // Attacking Timer runterzählen
    private void CountdownAttack()
    {
        if (attackingTimer > 0f)
            attackingTimer -= Time.deltaTime;
        else
            currentSpeed = maxSpeed;
    }
}
