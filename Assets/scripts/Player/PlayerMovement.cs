using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController Player;
    [SerializeField] private Transform maincam;
    [SerializeField] private Transform maincamparent;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    private Vector2 direction = Vector2.zero;
    private Vector3 move = Vector3.zero;
    private float gravityForce = -9.81f;
    private float currentRunSpeedMultiplier = 1.0f;
    private float walkSpeed = 5.0f;
    private bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        move = currentRunSpeedMultiplier * walkSpeed * (Vector3.Normalize(maincam.right * direction.x + maincamparent.forward * direction.y));
        Player.Move(move * Time.deltaTime);
        // gravity stuff ------------------------
        velocity.y = GetGravityForce();
        Player.Move(velocity * Time.deltaTime);

    }
    private float GetGravityForce()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = gravityForce;
        }
        velocity.y += gravityForce * Time.deltaTime;

        return velocity.y;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
