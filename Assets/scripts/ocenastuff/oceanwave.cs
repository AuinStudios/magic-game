using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class oceanwave : MonoBehaviour
{
    [SerializeField] private int dimesion = 10;


    [SerializeField] private MeshFilter filtermesh;
    [SerializeField] private Mesh mesh;
    public octave[] Octaves;
    [SerializeField] private Mesh messh;
  
    // Start is called before the first frame update
    void Start()
    {
        // mesh.vertices = generatevertices();
        mesh.vertices = messh.vertices;
        mesh.triangles = messh.triangles;
        mesh.bounds = messh.bounds;
        mesh.RecalculateBounds();
    }
    private Vector3[] generatevertices()
    {
        var verts = new Vector3[(dimesion + 1) * (dimesion + 1)];

        for(int x = 0; x <= dimesion; x++)
        {
            for(int z = 0; z <= dimesion; z++)
            {
                verts[index(x, z)] = new Vector3(x, 0, z);
            }
        }
        return verts;
    }

    private int index( int x  , int z)
    {
        return x * (dimesion + 1) + z;
    }

    private int[] generatetris()
    {
        var tries = new int[mesh.vertices.Length * 6];
        for (int x = 0; x < dimesion; x++)
        {
            for (int z = 0; z < dimesion; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);
                tries[index(x, z) * 6 + 3] = index(x, z + 1);
                tries[index(x, z) * 6 + 4] = index(x, z);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }
        return tries;
    }
    [Serializable]
    public struct octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }
    // Update is called once per frame
   // void Update()
   // {
   //     var verts = mesh.vertices;
   //     for (int x = 0; x <= dimesion; x++)
   //     {
   //         for (int z = 0; z <= dimesion; z++)
   //         {
   //             var y = 0f;
   //             for(int o = 0; o < Octaves.Length;o++)
   //             {
   //                 var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x) / dimesion, (z * Octaves[o].scale.y) / dimesion * Mathf.PI * 2f);
   //                 y += MathF.Cos(perl +  Octaves[o].speed.magnitude * Time.time) * Octaves[o].height;
   //             }
   //             verts[index(x, z)] = new Vector3(x, y, z);
   //         }
   //     }
   // }
}
