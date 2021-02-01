using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnGizmos : MonoBehaviour
{
    Mesh mesh;
    private Vector3[] meshToDisplay;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        meshToDisplay = Pathfinding.GridGraph.sendToTask8;
        Debug.Log(meshToDisplay.Length);

        mesh = GetComponent<MeshFilter>().mesh;
        meshToDisplay = mesh.vertices;

        for (var i = 0; i < meshToDisplay.Length; i++)
        {
            meshToDisplay[i] += Vector3.up * Time.deltaTime;
        }

        // assign the local vertices array into the vertices array of the Mesh.
        mesh.vertices = meshToDisplay;
        mesh.RecalculateBounds();

        
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print("space key was pressed");
        }
    }
}
