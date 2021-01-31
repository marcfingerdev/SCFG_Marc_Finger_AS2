using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //Generate path
    Seeker seeker;

    //Stores the path
    Path path;

    //Referencing the target
    private GameObject target;

    //a reference to GridGraphObject
    GameObject graphParent;



    // Start is called before the first frame update
    void Start()
    {
        //find the parent node of the point graph
        graphParent = GameObject.Find("AStar");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();

        StartCoroutine(updateGraph());

        StartCoroutine(waitABit());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            EnemySpawner.snakelength++;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator updateGraph()
    {
        while (true)
        {
            graphParent.GetComponent<AstarPath>().Scan();
            yield return null;
        }

    }

    IEnumerator waitABit()
    {
        yield return new WaitForSeconds(3);

        target = GameObject.Find("PlayerHead");

        //Instance of seeker attached to gameobject
        seeker = GetComponent<Seeker>();

        //Generating first path
        path = seeker.StartPath(transform.position, target.transform.position);

        //Start the process of moving the AI block to the target
        StartCoroutine(MoveTowardsTarget(this.transform));
    }

    IEnumerator MoveTowardsTarget(Transform theAI)
    {
        while (true)
        {

            List<Vector3> pos = path.vectorPath;
            //Debug.Log("Positions Count: " + pos.Count);

            for (int counter = 0; counter < pos.Count; counter++)
            {
                if (pos[counter] != null)
                {
                    while (Vector3.Distance(theAI.position, pos[counter]) >= 0.5f)
                    {
                        //The AI block moves 1 unit towards the position in the pos list
                        theAI.position = Vector3.MoveTowards(theAI.position, pos[counter], 1f);

                        //Path refrshed everytime in case code is updated for target to move
                        path = seeker.StartPath(theAI.position, target.transform.position);

                        //Wait until new path is generated
                        yield return seeker.IsDone();

                        //The paths direction is updated
                        pos = path.vectorPath;

                        yield return new WaitForSeconds(0.3f);
                    }

                }
                path = seeker.StartPath(theAI.position, target.transform.position);
                yield return seeker.IsDone();
                pos = path.vectorPath;

            }
            yield return null;
        }
    }

}
