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
    private float timer = 0;
    public IEnumerator Camerashake( float duration  , float shakepower , Vector3 distancebetweenmagic)
    {
        float a = (maincam.position - distancebetweenmagic).magnitude;
        duration -=  (maincam.position - distancebetweenmagic).magnitude;
        duration /= (maincam.position - distancebetweenmagic).magnitude / shakepower;
       // Debug.Log((maincam.position - distancebetweenmagic).magnitude);
        while ( duration> 0)
        {
            timer += Time.deltaTime * 100;
           // timer -= (maincam.position - distancebetweenmagic).magnitude * 2;
           // timer = Mathf.Clamp(timer, , 100);
            Debug.Log(a);
            //timer += (maincam.position - distancebetweenmagic).magnitude * 0.5f;
            maincam.position = Vector3.Lerp(maincam.position, new Vector3(maincam.position.x + Mathf.Cos(timer) - a / 1000 / shakepower, maincam.position.y + Mathf.Sin(timer) - a / 1000 / shakepower, maincam.position.z), 10 * Time.deltaTime) ;
            duration--;
            yield return new WaitForFixedUpdate();
        }
        timer = 0;
        maincam.localPosition = new Vector3(0, 0, maincam.localPosition.z);
    }
}
