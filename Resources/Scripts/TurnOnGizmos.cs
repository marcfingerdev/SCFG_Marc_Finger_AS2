using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnGizmos : MonoBehaviour
{
    Mesh mesh;
    private Vector3[] vertices;
    private int noOfTriangles;
    //int[] triangles;
    //public Path path;
    //private LineRenderer lr;
    //public List<Vector3> vectorPath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(theBoss());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator theBoss()
    {
        yield return new WaitForSeconds(2);

        vertices = GridGraph.sendToTask8;
        Vector2[] vertices2 = new Vector2[vertices.Length];

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices2[i] = vertices[i];
        }

        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices3 = new Vector3[vertices2.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices3[i] = new Vector3(vertices3[i].x, vertices3[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices3;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        gameObject.AddComponent(typeof(MeshRenderer));
        MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;

        yield break;
    }

}
