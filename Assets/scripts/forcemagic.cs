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
   // private float bbbb;
    private bool onlyonce = false;
    private int savecurrent = 0;
  [SerializeField]  private SphereCollider sphereradius;
    // Start is called before the first frame update
    void Awake()
    {
        savecurrent = Magic.Instance.currentcount;
       // float a = (0.5f/ 100) * Magic.Instance.magicmoves[savecurrent].radiuspercentage;
          float b = transform.localScale.x /4;
        //    b -= transform.localScale.x;
        float a = (b + Magic.Instance.magicmoves[savecurrent].radiuspercentage/ 100    ) - Magic.Instance.magicmoves[savecurrent].spread / 1000 / b  ;
        a *= a / Magic.Instance.magicmoves[savecurrent].howmanytimestoshoot ;
        a = a <= 0.1 ? a += Magic.Instance.magicmoves[savecurrent].radiuspercentage/ 1000 : a;
        //a *= Magic.Instance.magicmoves[savecurrent].radiuspercentage;
        // float a = b   - Magic.Instance.magicmoves[savecurrent].spread/ Magic.Instance.magicmoves[savecurrent].howmanytimestoshoot ;
        //  a = Mathf.Abs(a);
        // a /= 10;
        Debug.Log(a);
      //  a =  Mathf.Clamp(a, 0.1f, 0.5f);
       
        transform.localScale = new Vector3(a,a,a);
        
           
      //  a = Mathf.Abs(a);
       // float d = (Magic.Instance.magicmoves[savecurrent].spread / 100) * b;
        // a /= 2;
        
        force.AddForce(transform.forward * Magic.Instance.magicmoves[savecurrent].speed + transform.right * Random.Range(-Magic.Instance.magicmoves[savecurrent].spread , Magic.Instance.magicmoves[savecurrent].spread) / Magic.Instance.magicmoves[savecurrent].radiuspercentage + transform.up * Random.Range(-Magic.Instance.magicmoves[savecurrent].spread, Magic.Instance.magicmoves[savecurrent].spread) / Magic.Instance.magicmoves[savecurrent].radiuspercentage, ForceMode.Impulse);
            explosioneffect.gameObject.SetActive(false);
            explosioneffect.Stop();
       
        Destroy(gameObject, 4);
    }
    private void Update()
    {

        if (Input.GetKeyUp(Magic.Instance.typeofmagic[savecurrent]) && Magic.Instance.magicmoves[savecurrent].detonateion == true && onlyonce == false)
        {
           explosion();
          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Magic") && onlyonce == false)
        {
            explosion();
           // Debug.Log("a");
        }


    }
    
    public   void explosion()
    {
        mesh.enabled = false;
        var particle = explosioneffect.main;
        onlyonce = true;
        float a = (Magic.Instance.magicmoves[savecurrent].radiussize / 100) * Magic.Instance.magicmoves[savecurrent].radiuspercentage;
        //Debug.Log(a);
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

        sphereradius.radius = a;
        camerashake.Instance.startcamerashake(transform.position, Magic.Instance.magicmoves[savecurrent].camerarotationpower, Magic.Instance.magicmoves[savecurrent].distancecamerashake, Magic.Instance.magicmoves[savecurrent].camerashakePower, sphereradius);
        explosioneffect.gameObject.SetActive(true);
        explosioneffect.Play();
        firestop.Stop(false);
        force.velocity = Vector3.zero;
        Destroy(gameObject, 0.5f);
        
    
        
    }
    private void OnDrawGizmos()
    {
        if (explosioneffect != null && sphereradius != null)
        {
            Gizmos.DrawWireSphere(explosioneffect.transform.position, sphereradius.radius);
        }

    }
}
