using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Magic", menuName = "New Magic")]
public class MagicScriptableobject : ScriptableObject
{
    [Header("editing magic stuff")]
    [Range(10 ,100)]
    public float radiuspercentage = 100.0f;
    public float radiussize = 3.0f;
    [Header("Extra stuff")]
    public bool pullornot = false;
    public bool detonateion = false;
    [Header("Beam")]
    public bool beammode = false;
    public float timeruntllstop = 10.0f;
    [Range(10,100)]
    public float lengthpercentage = 100.0f;
    [Header("camera shake stuff")]
    public float camerashakePower = 2.0f;
    public float distancecamerashake = 30.0f;
    public float camerarotationpower = 3.0f;
    [Header("GetTypeOfMagic")]
    public GameObject typeofmagic;
    public Collider test;
    [Header("stats")]
    public int damage =5;
    
    public int speed = 30;
}
