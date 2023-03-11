using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Magic", menuName = "New Magic")]
public class MagicScriptableobject : ScriptableObject
{
    public int damage =5;
    public float camerashakePower = 1.2f;
    public float distancecamerashake = 10.0f;
    public float camerarotationpower = 5.0f;
    public int speed = 30;
}
