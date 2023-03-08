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
    private Vector3 movedash = Vector3.zero;
   // [SerializeField] private float smoothness = 5.0f;
    [Header("Quaternions")]
    private Quaternion playerot = Quaternion.identity;
    [Header("gravity")]
    private float gravityForce = -9.81f;
    [Header("runspeed and mutlpler")]
    [SerializeField]
    private float currentRunSpeedMultiplier = 1.0f;
    private float maxspeed = 20.0f;
    private float normalmaxspeed = 7.0f;
    private float accl = 15.0f;
    private float deaccl = 0.0f;
    private float dashtimer = 0.0f;
    private float dashcooldown = 6.0f;
    [Header("Checkifgrounded")]
    private bool isGrounded = false;
    [Header("Jump")]
    private float JumpForce = 5;
    // Update is called once per frame
    void Update()
    {

        direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        deaccl = deaccl < 0 ? deaccl = 0 : deaccl;
        
        dashcooldown = dashcooldown > 0 ? dashcooldown -= 5.0f * Time.deltaTime : dashcooldown = 0.0f;
        dashtimer =  dashtimer > 0? dashtimer -= 10.0f * Time.deltaTime: dashtimer = 0.0f;
        if ( Input.GetKeyDown(KeyCode.LeftShift) && dashcooldown <= 0 )
        {
            dashcooldown = 6.0f;
            deaccl = normalmaxspeed;
            dashtimer = 1f;
            movedash = currentRunSpeedMultiplier * maxspeed * 4 * (Vector3.Normalize(maincam.transform.right * direction.x + maincamparent.forward * direction.y));
        }

        if (direction.x != 0 && dashtimer <= 0.0f || direction.y != 0 && dashtimer <= 0.0f)
        {
         //   deaccl = Mathf.Clamp(deaccl, 0, normalmaxspeed);
            deaccl =   deaccl < normalmaxspeed ? deaccl += accl * Time.deltaTime : deaccl -= accl * Time.deltaTime;
           // Debug.Log(deaccl);
            move = currentRunSpeedMultiplier * normalmaxspeed * (Vector3.Normalize(maincam.transform.right * direction.x + maincamparent.forward * direction.y));
        }
        else if(deaccl > 0)
        {
            deaccl -= accl * Time.deltaTime;
            
        }

        if (direction.x != 0 && maincam.shiftlock == false || direction.y != 0 && maincam.shiftlock == false)
        {
            playerot = Quaternion.LookRotation(move);
          
            transform.rotation = Quaternion.Slerp(transform.rotation, playerot, 11 * Time.deltaTime);
        }
        // move charater ---------------------------
        if(dashtimer <= 0)
        {
          Player.Move(  deaccl *  move * Time.deltaTime);
        }
        else if( dashtimer > 0.0f)
        {
           // deaccl -= accl * 20 * Time.deltaTime;
            Player.Move( deaccl *   movedash * Time.deltaTime);
            dash();
        }
        // gravity stuff ------------------------
        velocity.y = GetGravityForce();

        Player.Move(velocity * Time.deltaTime);

Debug.Log(deaccl);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = JumpForce;
        }
    }


    private void dash()
    {
        if( deaccl > normalmaxspeed - 2 )
        {
            deaccl -= accl * 20  * Time.deltaTime;
           
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