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
    public void startcamerashake( Vector3 distancebetweenmagic , float distance , float shakepower)
    {
        StartCoroutine(Camerashake(30, distance, shakepower, distancebetweenmagic));
    }
    public IEnumerator Camerashake( float duration  ,float distance ,  float shakepower , Vector3 distancebetweenmagic)
    {
        float a = (maincam.position - distancebetweenmagic).magnitude / distance;
        while ( duration>  duration /2)
        {
            timer.x = Random.Range(-1.0f,1.0f) ;
            timer.y = Random.Range(-1.0f, 1.0f);
            Debug.Log(timer);
            maincam.position = Vector3.Lerp(maincam.position, new Vector3(maincam.position.x + Mathf.Sin(timer.x) * a / 10 * shakepower   , maincam.position.y + Mathf.Sin(timer.y) * a   / 10  * shakepower,  maincam.position.z),  Time.deltaTime * 20) ;
            maincam.localRotation = Quaternion.Slerp(maincam.localRotation, Quaternion.Euler(0,  Mathf.Sin(timer.x) * a * 2  ,  Mathf.Sin(timer.y) * a * 2),  Time.deltaTime * 20);
         //   Debug.Log(duration);
            duration--;
            yield return new WaitForFixedUpdate();
        }
        while(duration >0 && duration < duration / 2)
        {
            maincam.localRotation = Quaternion.Slerp(maincam.localRotation, Quaternion.Euler(0, 0,0), Time.deltaTime * 20);
            maincam.position = Vector3.Lerp(maincam.position, new Vector3(0,0, maincam.position.z), Time.deltaTime * 20);
            duration--;
            yield return new WaitForFixedUpdate();
        }
        maincam.localRotation = Quaternion.Euler(0, 0, 0);
        maincam.localPosition = new Vector3(0, 0, maincam.localPosition.z);
        timer = Vector2.zero;
    }
}
