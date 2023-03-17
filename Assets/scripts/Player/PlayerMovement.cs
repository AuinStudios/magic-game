using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    public static PlayerMovement Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    [SerializeField] private CharacterController Player;
    [SerializeField] private Camerafollow maincam;
    [SerializeField] private Transform maincamparent;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
     private Vector3 velocity = Vector3.zero;
    [Header("Vectors")]
    private Vector2 direction = Vector2.zero;
    private Vector3 move = Vector3.zero;

    private Vector3 movedash = Vector3.zero;

    private Vector3 frictionlerp = Vector3.zero;
    [Header("Quaternions")]
    private Quaternion playerot = Quaternion.identity;
    [Header("gravity")]
    private float gravityForce = -9.81f;
    [Header("runspeedmultiplier")]
    [SerializeField]
    private float currentRunSpeedMultiplier = 1.0f;
    private float maxspeed = 20.0f;
    private float normalmaxspeed = 7.0f;
    [Header("accleration")]
    private float acclrate = 15.0f;
    private float deaccl = 0.0f;
    [Header("Dash")]
    private float dashtimer = 0.0f;
    private float dashcooldown = 6.0f;
    [Header("Friction")]
    private float friction = 4.5f;
    [Header("Coyote time")]
    private float coyotetime = 0.2f;
    private float coyotetimecounter;
    [Header("JumpBuffer")]
    private float jumpbuffertime = 0.2f;
    private float jumpbuffercounter;
    [Header("Checkifgrounded")]
    private bool isGrounded = false;
    [Header("Jump")]
    private float JumpForce = 5;


   // [Header("test")]
   // [HideInInspector] public bool test;
   // [HideInInspector] public Vector3 test2;
    // Update is called once per frame
    void Update()
    {

        direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        deaccl = deaccl < 0 ? deaccl = 0 : deaccl;
        frictionlerp = deaccl <= 2? Vector3.zero : frictionlerp;
        dashcooldown = dashcooldown > 0 ? dashcooldown -= 5.0f * Time.deltaTime : dashcooldown = 0.0f;
        dashtimer =  dashtimer > 0? dashtimer -= 10.0f * Time.deltaTime: dashtimer = 0.0f;
        
        frictionlerp = Vector3.Lerp(frictionlerp , move, friction * Time.deltaTime);
        
        if ( Input.GetKeyDown(KeyCode.LeftShift) && dashcooldown <= 0 )
        {
            dashcooldown = 6.0f;
            deaccl = maxspeed;
            dashtimer = 1f;
            movedash = currentRunSpeedMultiplier * maxspeed * 2 * (Vector3.Normalize(maincam.transform.right * direction.x + maincamparent.forward * direction.y));
            
        }

        if (direction.x != 0 && dashtimer <= 0.0f || direction.y != 0 && dashtimer <= 0.0f)
        {
            deaccl =   deaccl < normalmaxspeed ? deaccl += acclrate * Time.deltaTime : deaccl -= acclrate * Time.deltaTime;
             
            move = currentRunSpeedMultiplier * normalmaxspeed * (Vector3.Normalize(maincam.transform.right * direction.x + maincamparent.forward * direction.y));
            
        }
        else if(deaccl > 0)
        {
            deaccl -= acclrate * 1.2f * Time.deltaTime;
            
        }

        if (direction.x != 0 && maincam.shiftlock == false || direction.y != 0 && maincam.shiftlock == false)
        {
            playerot = Quaternion.LookRotation(move);
          
            transform.rotation = Quaternion.Slerp(transform.rotation, playerot, 11 * Time.deltaTime);
        }


        if (!isGrounded )
        {
            coyotetimecounter -= Time.deltaTime;
            deaccl -= acclrate / 10 * Time.deltaTime;
        }
        else
        {
            coyotetimecounter = coyotetime;
        }
        // move charater ---------------------------

        if(dashtimer <= 0)
        {
          Player.Move( deaccl * frictionlerp   * Time.deltaTime);
        }
        else if( dashtimer > 0.0f)
        {
            // deaccl -= accl * 20 * Time.deltaTime;
            Player.Move( deaccl *   movedash * Time.deltaTime);
            dash();
        }

        // gravity stuff ------------------------
        //  if(test != true)
        //  {
        //    velocity.y = GetGravityForce();
        //     // Debug.Log(test);
        //    Player.Move(velocity * Time.deltaTime);
        //  }
        //  else
        //  {
        //      velocity.y = test2.y;
        //    //  Debug.Log(test2);
        //      Player.Move(velocity * Time.deltaTime);
        //  }
        velocity.y = GetGravityForce();
        // Debug.Log(test);
        Player.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpbuffercounter = jumpbuffertime;
            
        }
        else
        {
            jumpbuffercounter -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyotetimecounter = 0.0f;
        }
        if( coyotetimecounter > 0.0f && jumpbuffercounter > 0.0f)
        {
           velocity.y = JumpForce;
        }
    }
    private void dash()
    {
        if( deaccl > normalmaxspeed - 2 )
        {
            deaccl -= acclrate * 20  * Time.deltaTime;
           
        }
    }

    private float GetGravityForce()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = 0;
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