using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcemagic : MonoBehaviour
{
    [SerializeField] private Rigidbody force;
    [SerializeField] private ParticleSystem explosioneffect;
    [SerializeField] private ParticleSystem firestop;
    //[SerializeField] private MagicScriptableobject scriptableobject;
    [SerializeField] private SphereCollider explosionsizeshake;
    [SerializeField] private LayerMask layertobringinobjects;
    private bool onlyonce = false;
    private int savecurrent = 0;
    //private GameObject broke;
    // Start is called before the first frame update
    void Awake()
    {
        savecurrent = Magic.Instance.currentcount;
        explosioneffect.gameObject.SetActive(false);
        explosioneffect.Stop();
        force.AddForce(transform.forward * Magic.Instance.magicmoves[savecurrent].speed, ForceMode.Impulse);
        Destroy(gameObject, 4);
        // Destroy(gameObject, 3);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && Magic.Instance.magicmoves[savecurrent].detonateion == true && onlyonce == false)
        {
            explosion();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Magic") && onlyonce == false)
        {
            explosion();
        }


    }

    public void explosion()
    {

        var particle = explosioneffect.main;
        onlyonce = true;
        float a = (explosionsizeshake.radius / 100) * Magic.Instance.magicmoves[savecurrent].radiuscollider;
        // particle.startSpeed = scriptableobject.pullornot == false ? particle.startSpeed = 20.0f : particle.startSpeed = -20.0f;
        if (Magic.Instance.magicmoves[savecurrent].pullornot == true)
        {
            particle.startSpeed = -20.0f;
            var colliders = Physics.OverlapSphere(explosioneffect.transform.position, a, layertobringinobjects) ;
            foreach (Collider test in colliders)
            {
                test.transform.position = test.gameObject.isStatic == false ? test.transform.position = explosioneffect.transform.position : test.transform.position;
            }
        }
        else
        {
            particle.startSpeed = 20.0f;
        }

        explosionsizeshake.radius = a;
        camerashake.Instance.startcamerashake(transform.position, Magic.Instance.magicmoves[savecurrent].camerarotationpower, Magic.Instance.magicmoves[savecurrent].distancecamerashake, Magic.Instance.magicmoves[savecurrent].camerashakePower, explosionsizeshake);
        explosioneffect.gameObject.SetActive(true);
        explosioneffect.Play();
        firestop.Stop(false);
        force.velocity = Vector3.zero;
        Destroy(gameObject, 0.5f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(explosioneffect.transform.position, explosionsizeshake.radius);
    }
}
