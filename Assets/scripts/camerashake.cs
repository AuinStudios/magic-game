using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    #region Singleton
    public static camerashake Instance { get; private set; }
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
    [SerializeField] private Transform maincam;
    private Vector2 timer = Vector2.zero;
    public void startcamerashake( Vector3 distancebetweenmagic, float rotationpower , float distance , float shakepower , SphereCollider size)
    {
        StartCoroutine(Camerashake(30, distance, shakepower,  rotationpower, distancebetweenmagic , size));
    }
    public IEnumerator Camerashake( float duration  ,float distance ,float rotationpower  , float shakepower , Vector3 distancebetweenmagic , SphereCollider size)
    {
        float a = (maincam.position - distancebetweenmagic).magnitude;
        float force = (1 - Mathf.Clamp01(a / distance));
        duration = duration - a ;
       
        while ( duration > 0)
        {
          //  timer.x += Time.deltaTime * force;
           timer.x = Mathf.Lerp(timer.x , Random.Range(-1.0f,1.0f) ,  Time.deltaTime * 10) ;
           timer.y = Mathf.Lerp(timer.y, Random.Range(-1.0f, 1.0f),  Time.deltaTime* 10);
           //  Debug.Log(Mathf.Sin(timer.x) * force * size.radius  );
            maincam.position = Vector3.Lerp(maincam.position, new Vector3(maincam.position.x +  Mathf.Sin(timer.x) * force /shakepower ,maincam.position.y + Mathf.Sin(timer.y) * force /shakepower,  maincam.position.z),  Time.deltaTime * 20) ;
            maincam.localRotation = Quaternion.Slerp(maincam.localRotation, Quaternion.Euler(0,  Mathf.Sin(timer.x) * force * rotationpower   ,  Mathf.Sin(timer.y) * -force * rotationpower),  Time.deltaTime * 20);
            duration--;
            yield return new WaitForFixedUpdate();
        }
        float timercomeback = 0.0f;
        while(timercomeback <   0.5f)
        {
            timercomeback += 1.0f * Time.deltaTime;
           // Debug.Log(timercomeback);
            maincam.localRotation = Quaternion.Slerp(maincam.localRotation, Quaternion.Euler(0, 0,0), timercomeback /0.5f);
            maincam.localPosition = Vector3.Lerp(maincam.localPosition, new Vector3(0,0, maincam.localPosition.z), timercomeback / 0.5f);
            duration--;
            yield return new WaitForFixedUpdate();
        }
       // maincam.localRotation = Quaternion.Euler(0, 0, 0);
       // maincam.localPosition = new Vector3(0, 0, maincam.localPosition.z);
        timer = Vector2.zero;
    }
}
