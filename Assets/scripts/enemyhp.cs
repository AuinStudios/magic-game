using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health = 100.0f;
    [SerializeField] private Transform maincam;
    [SerializeField] private SpriteRenderer healthbar;
    [SerializeField] private MagicScriptableobject scriptableobject;
    private Vector3 distance;
    private Vector2 test;
    //private Quaternion lookat;
    void Start()
    {
        test = healthbar.size;
    }

    // Update is called once per frame
    void Update()
    {
        distance = healthbar.transform.parent.position - maincam.position;
        healthbar.transform.parent.rotation = Quaternion.LookRotation(distance);
        healthbar.size = Vector2.Lerp(healthbar.size, test, 10.0f * Time.deltaTime);
            
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magic"))
        {
            // StopCoroutine(healthdown());
            health -= scriptableobject.damage;
            health = Mathf.Clamp(health, 0, 100);
            test.x =  -health / 1000;
        }
    }


}
