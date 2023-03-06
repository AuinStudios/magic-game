using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController Player;
    [SerializeField] private Camerafollow maincam;
    [SerializeField] private Transform maincamparent;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [Header("Vectors")]
    private Vector2 direction = Vector2.zero;
    private Vector3 move = Vector3.zero;
    private Vector3 playerlookat = Vector3.zero;

    [Header("smoothmove")]
    private Vector3 smoothdamp = Vector3.one * 10;
    private Vector3 smoothmove = Vector3.zero;
    [SerializeField] private float smoothness = 5.0f;
    [Header("Quaternions")]
    private Quaternion playerot = Quaternion.identity;
    [Header("gravity")]
    private float gravityForce = -9.81f;
    [Header("runspeed and mutlpler")]
    [SerializeField]
    private float currentRunSpeedMultiplier = 1.0f;
   // private float walkSpeed = 5.0f;
    private float maxspeed = 5.0f;
    private float accl = 13.0f;
    private float deaccl = 0.0f;
  //[SerializeField]  private float accleration = 0.7f;
    [Header("Checkifgrounded")]
    private bool isGrounded = false;
    [Header("Jump")]
    private float JumpForce = 5;
    // Start is called before the first frame update
    //void Start()
    //{
    //    deaccl = maxspeed;
    //}

    // Update is called once per frame
    void Update()
    {

        direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (direction.x != 0 || direction.y != 0)
        {
            deaccl = Mathf.Clamp(deaccl, 0, maxspeed);
            deaccl = deaccl < maxspeed? deaccl += accl * Time.deltaTime : deaccl = maxspeed;
            move = currentRunSpeedMultiplier * maxspeed * (Vector3.Normalize(maincam.transform.right * direction.x + maincamparent.forward * direction.y));
        }
        else if(deaccl > 0)
        {
            deaccl -= accl * Time.deltaTime;
        }
        deaccl = deaccl < 0 ? deaccl = 0 : deaccl;
        Debug.Log(deaccl);
       // smoothmove = Vector3.SmoothDamp(Player.velocity, move, ref smoothdamp, smoothness * Time.deltaTime);
        if (direction.x != 0 && maincam.shiftlock == false || direction.y != 0 && maincam.shiftlock == false)
        {
            playerlookat = maincam.transform.position - transform.position;
            playerlookat = new Vector3(playerlookat.x, 0, playerlookat.z);
            playerot = Quaternion.LookRotation(-playerlookat);
          
            transform.rotation = Quaternion.Slerp(transform.rotation, playerot, 11 * Time.deltaTime);
        }

        // if(direction.x)
       // Vector3 speedif = move - Player.velocity;
      //  Vector3 test = speedif * accleration;
        Player.Move(  deaccl *  move * Time.deltaTime);
        // gravity stuff ------------------------
        velocity.y = GetGravityForce();

        Player.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = JumpForce;
        }
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
