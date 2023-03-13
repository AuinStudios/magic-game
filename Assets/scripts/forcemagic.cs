using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcemagic : MonoBehaviour
{
    [SerializeField] private Rigidbody force;
    [SerializeField] private ParticleSystem explosioneffect;
    [SerializeField] private ParticleSystem firestop;
    //[SerializeField] private MagicScriptableobject scriptableobject;
    // [SerializeField] private SphereCollider explosionsizeshake;
   // private Collider col;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private LayerMask layertobringinobjects;

    private bool onlyonce = false;
    private int savecurrent = 0;
    private SphereCollider sphere;
    private BoxCollider box;
    // Start is called before the first frame update
    void Awake()
    {


        savecurrent = Magic.Instance.currentcount;
        if(Magic.Instance.magicmoves[savecurrent].test is SphereCollider)
        {
            sphere = (SphereCollider)Magic.Instance.magicmoves[savecurrent].test; 
            sphere.radius = 0.5f;
        }
        else if(Magic.Instance.magicmoves[savecurrent].test is BoxCollider)
        {
            box = (BoxCollider)Magic.Instance.magicmoves[savecurrent].test;
        }
       
        if (Magic.Instance.magicmoves[savecurrent].beammode == false)
        {
            force.AddForce(transform.forward * Magic.Instance.magicmoves[savecurrent].speed, ForceMode.Impulse);
            explosioneffect.gameObject.SetActive(false);
            explosioneffect.Stop();
        }
        else
        {
            var a = firestop.shape;
            a.length = (Magic.Instance.magicmoves[savecurrent].timeruntllstop / 100) * Magic.Instance.magicmoves[savecurrent].lengthpercentage;
            Destroy(gameObject, 0.5f);
        }
        // aa = (Magic.Instance.magicmoves[savecurrent].timeruntllstop / 100) * 10;
        // Debug.Log(aa);
        Destroy(gameObject, 4);
        // Destroy(gameObject, 3);
    }
    private void Update()
    {

        if (Input.GetKeyUp(Magic.Instance.typeofmagic[savecurrent]) && Magic.Instance.magicmoves[savecurrent].detonateion == true && onlyonce == false)
        {
            explosion();
          
        }
        // if (Magic.Instance.magicmoves[savecurrent].beammode == true)
        // {
        //     timeuntllstop += Magic.Instance.magicmoves[savecurrent].timeruntllstop * Time.deltaTime;
        //     if (timeuntllstop > aa)
        //     {
        //         force.velocity = Vector3.zero;
        //     }
        // }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Magic") && onlyonce == false && Magic.Instance.magicmoves[savecurrent].beammode == false)
        {
            explosion();
            Debug.Log("a");
        }


    }

    public void explosion()
    {
        mesh.enabled = false;
        var particle = explosioneffect.main;
        onlyonce = true;
        float a = (Magic.Instance.magicmoves[savecurrent].radiussize / 100) * Magic.Instance.magicmoves[savecurrent].radiuspercentage;
        if (Magic.Instance.magicmoves[savecurrent].pullornot == true)
        {
            particle.startSpeed = -20.0f;
            var colliders = Physics.OverlapSphere(explosioneffect.transform.position, a, layertobringinobjects);
            foreach (Collider test in colliders)
            {
                test.transform.position = test.gameObject.isStatic == false ? test.transform.position = explosioneffect.transform.position : test.transform.position;
            }
        }
        else
        {
            particle.startSpeed = 20.0f;
        }

        
        camerashake.Instance.startcamerashake(transform.position, Magic.Instance.magicmoves[savecurrent].camerarotationpower, Magic.Instance.magicmoves[savecurrent].distancecamerashake, Magic.Instance.magicmoves[savecurrent].camerashakePower, sphere);
        explosioneffect.gameObject.SetActive(true);
        explosioneffect.Play();
        firestop.Stop(false);
        force.velocity = Vector3.zero;
        Destroy(gameObject, 0.5f);
        sphere.radius = a;
    }

    private void OnDrawGizmos()
    {
        if (explosioneffect != null)
        {
            Gizmos.DrawWireSphere(explosioneffect.transform.position, sphere.radius);
        }

    }
}
