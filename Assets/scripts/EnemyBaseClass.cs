using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;
    [SerializeField]
    private Transform maincam;
    [SerializeField]
    private SpriteRenderer healthbar;
    [SerializeField]
    private MagicScriptableobject scriptableobject;

    private Vector3 distance;
    private Vector2 test;

    public virtual void Start()
    {
        test = healthbar.size;
    }

    public virtual void Update()
    {
        distance = healthbar.transform.parent.position - maincam.position;
        healthbar.transform.parent.rotation = Quaternion.LookRotation(distance);
        healthbar.size = Vector2.Lerp(healthbar.size, test, 10.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(other);
    }

    public virtual void TakeDamage(Collider other)
    {
        if (other.CompareTag("Magic"))
        {
            // StopCoroutine(healthdown());
            health -= scriptableobject.damage;
            health = Mathf.Clamp(health, 0, 100);
            test.x = -health / 1000;
        }
    }
}
