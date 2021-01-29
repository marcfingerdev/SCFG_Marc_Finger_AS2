using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionRecord2

{
    Vector3 position;

    GameObject breadcrumbBox;

    int positionOrder;

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        positionRecord2 p = obj as positionRecord2;
        if ((System.Object)p == null)
            return false;
        return position == p.position;
    }


    public bool Equals(positionRecord2 o)
    {
        if (o == null)
            return false;


        //the distance between any food spawned
        return Vector3.Distance(this.position, o.position) < 2f;


    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }




    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}

public class EnemySpawner : MonoBehaviour
{

    GameObject EnemyBox, EnemyStart, breadcrumbBox2, pathParent2;

    List<positionRecord2> pastPositions;

    public static int snakelength = 4;

    int positionorder;

    bool firstrun = true;

    // Start is called before the first frame update
    void Start()
    {
        //player snake
        EnemyStart = GameObject.Find("EnemyStart");

        EnemyBox = Instantiate(Resources.Load<GameObject>("Prefabs/Box"), EnemyStart.transform.position, Quaternion.identity);

        EnemyBox.GetComponent<SpriteRenderer>().color = Color.red;

        EnemyBox.AddComponent<EnemyAI>();

        EnemyBox.AddComponent<Seeker>();

        EnemyBox.name = "EnemyHead";

        breadcrumbBox2 = Resources.Load<GameObject>("Prefabs/Box");

        pastPositions = new List<positionRecord2>();

        pathParent2 = new GameObject();

        pathParent2.transform.position = new Vector3(0f, 0f);

        pathParent2.name = "Path Parent2";

        drawTail(snakelength);

        StartCoroutine(waitForAI());
    }

    // Update is called once per frame
    void Update()
    {

            
    }

    IEnumerator waitForAI()
    {
        while (true){
            savePosition();

            drawTail(snakelength);

            yield return new WaitForSeconds(0.3f);
        }
        
    }

    void drawTail(int length)
    {
        clearTail();

        if (pastPositions.Count > length)
        {
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;


            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox2, pastPositions[snakeblocks].Position, Quaternion.identity);

            }

        }

        if (firstrun)
        {

            //I don't have enough positions in the past positions list
            for (int count = length; count > 0; count--)
            {
                positionRecord2 fakeBoxPos = new positionRecord2();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);
                pastPositions.Add(fakeBoxPos);




            }
            firstrun = false;
            drawTail(length);
        }

    }

    void clearTail()
    {
        foreach (positionRecord2 p in pastPositions)
        {
            // Debug.Log("Destroy tail" + pastPositions.Count);
            Destroy(p.BreadcrumbBox);
        }
    }

    void savePosition()
    {
        positionRecord2 currentBoxPos = new positionRecord2();

        currentBoxPos.Position = EnemyBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(EnemyBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox2, EnemyBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent2.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }
    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (positionRecord2 p in pastPositions)
        {

            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    //this breaks the foreach so I don't need to keep checking
                    return true;
                }
            }
        }

        return false;
    }



    void cleanList()
    {
        for (int counter = pastPositions.Count - 1; counter > pastPositions.Count; counter--)
        {
            pastPositions[counter] = null;
        }
    }

}
