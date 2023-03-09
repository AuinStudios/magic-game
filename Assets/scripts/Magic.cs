using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] private Transform MainCamParent;
    [SerializeField] private Transform camerarot;
    [SerializeField] private GameObject magic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Quaternion e = Quaternion.Euler(camerarot.eulerAngles.x - 1, MainCamParent.eulerAngles.y, 0);
            //Debug.Log(e);

           Instantiate(magic , MainCamParent.position + new Vector3(0.5f,1,0) , e );
        }
    }

}
