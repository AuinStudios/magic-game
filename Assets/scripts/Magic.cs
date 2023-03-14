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
    public List<KeyCode> typeofmagic;
  [HideInInspector]  public int currentcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < magicmoves.Count; i++)
        //{
        //    float a = (5.0f) * magicmoves[i].howmanytimestoshoot / magicmoves[i].radiuspercentage;
        //   // float b = a / magicmoves[i].howmanytimestoshoot - magicmoves[i].spread * 10;
        //    // int e  = a / magicmoves[i].radiuspercentage;
        //    // a *= 1000;
        //    //  e *= 1000;
        //    Debug.Log(a);
        //    magicmoves[i].damage = a;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < typeofmagic.Count; i++)
        {
            if (Input.GetKeyDown(typeofmagic[i]))
            {
                currentcount = i;

                StartCoroutine(summon(i));

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
    private IEnumerator summon(int i)
    {
       // yield return new WaitForSeconds(i / 10);
       
        if(magicmoves[i].howmanytimestoshoot >= 3  && magicmoves[i].spread >= 10 && magicmoves[i].radiuspercentage != 100)
        {
            
            float force = magicmoves[i].howmanytimestoshoot * magicmoves[i].spread ;
           // float force2 = magicmoves[i].howmanytimestoshoot / magicmoves[i].radiuspercentage;
            force /= magicmoves[i].radiuspercentage / 10;
            force /= 2;
            Debug.Log(magicmoves[i].howmanytimestoshoot + force  / 10 );
            for (int b = 0; b < magicmoves[i].howmanytimestoshoot + force / 10  ; b++)
            {
                Quaternion e = Quaternion.Euler(camerarot.eulerAngles.x - 1, MainCamParent.eulerAngles.y, 0);
                Instantiate(magicmoves[i].typeofmagic, MainCamParent.position + new Vector3(0, 1.5f, 0), e);
                yield return null;
            }
        }
        else
        {
            for (int b = 0; b < magicmoves[i].howmanytimestoshoot; b++)
            {
                 Quaternion e = Quaternion.Euler(camerarot.eulerAngles.x - 1, MainCamParent.eulerAngles.y, 0);
               Instantiate(magicmoves[i].typeofmagic, MainCamParent.position + new Vector3(0, 1.5f, 0), e);
               // test.transform.localScale = new Vector3(Mathf.Clamp(test.transform.localScale.x, test.transform.localScale.x / 2, test.transform.localScale.x * 2), Mathf.Clamp(test.transform.localScale.y, test.transform.localScale.y / 2, test.transform.localScale.y * 2), Mathf.Clamp(test.transform.localScale.z, test.transform.localScale.z / 2, test.transform.localScale.z * 2));
                yield return new WaitForSeconds(0.2f);
            }
        }
      
    }
}
