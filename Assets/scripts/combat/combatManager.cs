using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
   [SerializeField] private bool once = false;
    [SerializeField] private AnimationCurve animcurv;
    [SerializeField] private Animation animcurvcilp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // once = anim.GetCurrentAnimatorStateInfo(0).IsName("animswordmoment") ? once = true : once = false;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("attack");
           
         
        }
        
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !once)
        {
            
            once = true;
          // anim.speed = 0;
            //  anim.GetCurrentAnimatorClipInfoCount(0);

            anim.CrossFade("animswordmoment", 0.2f, 0 );
            anim.Update(0.0f);
           // anim.Play("New State");
            Debug.Log('a');
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
