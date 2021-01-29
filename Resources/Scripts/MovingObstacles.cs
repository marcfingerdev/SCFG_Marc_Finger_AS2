using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    public List<Transform> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObstacle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveObstacle()
    {
        while (true) { 
            foreach (Transform mytransform in waypoints)
            {
                while (Vector3.Distance(gameObject.transform.position, mytransform.position) > 0.1f)
                {
                    //1 unit towards the first one
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                    mytransform.position, 1f);

                    yield return new WaitForSeconds(0.3f);
                }
            }
        }
    }

}
