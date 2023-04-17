using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
 


    [Header("transforms")]
    [SerializeField] private Transform camerarotation;
    [SerializeField] private Transform playerotation;
    [SerializeField] private Transform shiftlockoffset;

     private Vector3 input;

    [Header("smoothnessforcamera")]
    private Quaternion smoothmovementy = Quaternion.identity;
    private Quaternion smoothplayerrotation = Quaternion.identity;
    private Quaternion smoothmovementx = Quaternion.identity;
    [Header("cameraoffset")]
    [SerializeField] private float inputz = -6;
    [Header("collider stuff")]

    private Vector3 dollydir;
    private Vector3 cameradesiredpos;

    [Header("Bools")]
   [HideInInspector] public bool shiftlock = false;
    [Header("Floats")]
    private float collidervalue = 0.0f;
    private float maxdistance = -6;
    private float mindistance = -1;
    [Header("Mouse Texture")]
    [SerializeField] private Texture2D MouseTexture;
    void Start()
    {
        //Vector2 centerPos = new Vector2(Screen.width / 2, 0);
        input = new Vector3(0, 2);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dollydir = transform.localPosition.normalized;
        collidervalue = transform.localPosition.magnitude;
        Cursor.SetCursor(MouseTexture, Vector2.zero,  CursorMode.Auto);
    }
    private void LateUpdate()
    {
        camerarotation.position =  playerotation.position + new Vector3(0,0.7f,0);
    }
    // Update is called once per frame
    void Update()
    {
        
        input.y += Input.GetAxisRaw("Mouse Y") * 100 * Time.fixedDeltaTime;
        inputz += Input.GetAxisRaw("Mouse ScrollWheel") * 100 * Time.fixedDeltaTime;

        inputz = Mathf.Clamp(inputz, maxdistance, mindistance);
        input.y = Mathf.Clamp(input.y, -30, 70);

        input.x = input.x > 360 || input.x < -360 ? input.x = 0 : input.x;
        // camera movement -----------------------------------------------------------------------------------------
        smoothmovementy = Quaternion.Euler(input.y, 0, 0);
        smoothmovementx = Quaternion.Euler(0, -input.x, 0);
        camerarotation.rotation = Quaternion.Slerp(camerarotation.rotation, smoothmovementx, Time.deltaTime * 11);
        shiftlockoffset.localRotation = Quaternion.Slerp(shiftlockoffset.localRotation, smoothmovementy, Time.deltaTime * 11);
        

        shiftlockoffset.localPosition = shiftlock == true ? Vector3.right : Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isshiftlocked(shiftlock);
        }
        else if (shiftlock == true)
        {
            input.x += Input.GetAxisRaw("Mouse X") * 100 * Time.fixedDeltaTime;
            smoothplayerrotation = Quaternion.Euler(playerotation.rotation.x, -input.x, playerotation.rotation.z);
            playerotation.rotation = Quaternion.Lerp(playerotation.rotation, smoothplayerrotation, 11 * Time.deltaTime);
            cameradesiredpos = shiftlockoffset.TransformPoint(new Vector3(dollydir.x * -maxdistance , transform.localPosition.y * -maxdistance, dollydir.z * -maxdistance));
        }
        else
        {
            cameradesiredpos = shiftlockoffset.TransformPoint(new Vector3(transform.localPosition.x * -maxdistance, transform.localPosition.y * -maxdistance, dollydir.z * -maxdistance));
            input.x += Input.GetAxisRaw("Mouse X") * 100 * Time.fixedDeltaTime;
        }

        if (Physics.Linecast(camerarotation.position, cameradesiredpos, out RaycastHit hitt))
        {

            collidervalue = Mathf.Clamp(-hitt.distance * 0.85f, maxdistance, mindistance);

            if (-hitt.distance >= inputz)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, collidervalue), 100 * Time.deltaTime);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, inputz), 11 * Time.deltaTime);
            }

        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, inputz), 11 * Time.deltaTime);
        }

    }

    private bool isshiftlocked(bool shiftlockk)
    {
        shiftlockk = shiftlockk == true ? shiftlockk = false : shiftlockk = true;
        shiftlock = shiftlockk;
        Cursor.visible = shiftlockk;
        return shiftlock;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(camerarotation.position, cameradesiredpos, Color.red, 0.2f);
    }
}