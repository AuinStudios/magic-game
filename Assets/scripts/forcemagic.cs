using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcemagic : MonoBehaviour
{
    [SerializeField] private Rigidbody force;
    [SerializeField] private ParticleSystem explosioneffect;
    [SerializeField] private ParticleSystem firestop;
    //private GameObject broke;
    // Start is called before the first frame update
    void Awake()
    {
        explosioneffect.Stop();
        force.AddForce(transform.forward * 30, ForceMode.Impulse);
        Destroy(gameObject, 4);
       // Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            explosioneffect.Play();
            firestop.Stop(false);
            force.velocity = Vector3.zero;
            Destroy(gameObject, 0.5f);
                 
          
            
            Debug.Log("a");
        }
        
        
    }
}
