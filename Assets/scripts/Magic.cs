using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Magic : MonoBehaviour
{
    #region Singleton
    public static Magic Instance { get; private set; }
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
    [SerializeField] private Transform MainCamParent;
    [SerializeField] private Transform camerarot;
    [SerializeField] private GameObject magic;
    public List<MagicScriptableobject> magicmoves;
    [SerializeField] private List<KeyCode> typeofmagic;
  [HideInInspector]  public int currentcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < typeofmagic.Count; i++)
        {
            if (Input.GetKeyDown(typeofmagic[i]))
            {
                currentcount = i;
                Quaternion e = Quaternion.Euler(camerarot.eulerAngles.x - 1, MainCamParent.eulerAngles.y, 0);
               
                Instantiate(magic , MainCamParent.position + new Vector3(0,1.5f,0) , e );
            }
        }
        //foreach (KeyCode kcode in typeofmagic)
        //{
        //    
        //    if (Input.GetKeyDown(kcode))
        //    {
        // 
        //        //Quaternion e = Quaternion.Euler(camerarot.eulerAngles.x - 1, MainCamParent.eulerAngles.y, 0);
        //        //
        //        // Instantiate(magic , MainCamParent.position + new Vector3(0,1.5f,0) , e );
        //
        //    }
        //}
       // if (Input.GetKeyDown(KeyCode.T))
       // {
       //     Quaternion e = Quaternion.Euler(camerarot.eulerAngles.x - 1, MainCamParent.eulerAngles.y, 0);
       //     //Debug.Log(e);
       //
       //    Instantiate(magic , MainCamParent.position + new Vector3(0,1.5f,0) , e );
       // }
    }

}
