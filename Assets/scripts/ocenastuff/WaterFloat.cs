using AuinDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class WaterFloat : MonoBehaviour
{
    //public properties
    [SerializeField] private float AirDrag = 1;
    [SerializeField] private float WaterDrag = 10;
    [SerializeField] private bool AffectDirection = true;
    [SerializeField] private bool isnotplayer = true;
   // [SerializeField] private 
    public Transform[] FloatPoints;
    [SerializeField] private bool isonwater = false;
    protected float timer = 1.0f;
    //used components
    protected Rigidbody Rigidbody;
    protected oceanwave Waves;

    //water line
    protected float WaterLine;
    protected Vector3[] WaterLinePoints;

    //help Vectors
    protected Vector3 smoothVectorRotation;
    protected Vector3 TargetUp;
    protected Vector3 centerOffset;

    public Vector3 Center { get { return transform.position + centerOffset; } }

    // Start is called before the first frame update
    void Awake()
    {
        //get components
        //Waves = FindObjectOfType<oceanwave>();
        if (GetComponent<Rigidbody>() != null)
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.useGravity = false;
        }


        //compute center
        WaterLinePoints = new Vector3[FloatPoints.Length];
        for (int i = 0; i < FloatPoints.Length; i++)
            WaterLinePoints[i] = FloatPoints[i].position;
        centerOffset = PhysicsHelper.GetCenter(WaterLinePoints) - transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //default water surface
        var newWaterLine = 0f;
        var pointUnderWater = false;

        //set WaterLinePoints and WaterLine
        if (isonwater == true)
        {
            for (int i = 0; i < FloatPoints.Length; i++)
            {
                //height
                WaterLinePoints[i] = FloatPoints[i].position;
                WaterLinePoints[i].y = Waves.GetHeight(FloatPoints[i].position);
                newWaterLine += WaterLinePoints[i].y / FloatPoints.Length;
                if (WaterLinePoints[i].y > FloatPoints[i].position.y)
                    pointUnderWater = true;
            }
            TargetUp = PhysicsHelper.GetNormal(WaterLinePoints);
        }


        var waterLineDelta = newWaterLine - WaterLine;
        WaterLine = newWaterLine;

        //compute up vector


        //gravity
        var gravity = Physics.gravity;
        if (Rigidbody != null)
        {
            Rigidbody.drag = AirDrag;
        }
        if (WaterLine > Center.y)
        {
            if (Rigidbody != null)
            {
                Rigidbody.drag = WaterDrag;
            }
            isonwater = true;
            timer = 3f;
            //under water
            if (isnotplayer)
            {
                //attach to water surface
                transform.position = new Vector3(transform.position.x, WaterLine - centerOffset.y, transform.position.z);
               
            }
            else
            {
                
              //  PlayerMovement.Instance.test = true;
                //go up
                gravity = AffectDirection ? TargetUp * -Physics.gravity.y : -Physics.gravity;
                transform.Translate(Vector3.up * waterLineDelta * 0.9f);
              //  PlayerMovement.Instance.test2 = gravity * Mathf.Clamp(Mathf.Abs(WaterLine - Center.y), 0, 1);

            }
        }
        else
        {
            if(Rigidbody == null)
            {
             // PlayerMovement.Instance.test = false;
            }
            
            timer -= 1.0f * Time.deltaTime;
            isonwater = timer <= 0 ? isonwater = false : isonwater;
        }
        if (Rigidbody != null)
        {
            Rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(WaterLine - Center.y), 0, 1));
        }

        //rotation
        if (pointUnderWater && Rigidbody != null)
        {
            //attach to water surface
            TargetUp = Vector3.SmoothDamp(transform.up, TargetUp, ref smoothVectorRotation, 0.2f);
            transform.rotation = Quaternion.FromToRotation(transform.up, TargetUp) * transform.rotation;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (FloatPoints == null)
            return;

        for (int i = 0; i < FloatPoints.Length; i++)
        {
            if (FloatPoints[i] == null)
                continue;

            if (Waves != null)
            {

                //draw cube
                Gizmos.color = Color.red;
                Gizmos.DrawCube(WaterLinePoints[i], Vector3.one * 0.3f);
            }

            //draw sphere
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(FloatPoints[i].position, 0.1f);

        }

        //draw center
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(Center.x, WaterLine, Center.z), Vector3.one * 1f);
            Gizmos.DrawRay(new Vector3(Center.x, WaterLine, Center.z), TargetUp * 1f);
        }
    }
}
