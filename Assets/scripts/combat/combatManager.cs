using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("attack");
           
          //  transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(40, -30, 0), 10 * Time.deltaTime);
        }
    }

   // private void attack()
   // {
   //     LeanTween.rotateLocal(gameObject, new Vector3(-40, 30, -20), 0.5f)
   //         .setEaseOutSine()
   //         .setOnComplete(() => { LeanTween.rotateLocal(gameObject, new Vector3(20, -22, 20), 0.5f).setEaseInSine(); });
   //         
   //         LeanTween.moveLocal(transform.parent.gameObject, Vector3.back * 0.1f, 0.5f)
   //         .setEaseOutSine()
   //         .setOnComplete(() => { LeanTween.moveLocal(transform.parent.gameObject, Vector3.forward * 0.2f, 0.5f).setEaseInSine(); });
   // }
}
