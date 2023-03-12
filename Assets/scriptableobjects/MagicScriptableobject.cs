using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Magic", menuName = "New Magic")]
public class MagicScriptableobject : ScriptableObject
{
    [Header("editing magic stuff")]
    [Range(10 ,100)]
    public float radiuscollider = 100.0f;
    public bool pullornot = false;
    public bool detonateion = false;
    [Header("camera shake stuff")]
    public float camerashakePower = 2.0f;
    public float distancecamerashake = 30.0f;
    public float camerarotationpower = 3.0f;
    [Header("stats")]
    public int damage =5;
    
    public int speed = 30;
}
