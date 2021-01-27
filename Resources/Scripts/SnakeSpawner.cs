using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionRecord

{
    Vector3 position;

    GameObject breadcrumbBox;

    int positionOrder;

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        positionRecord p = obj as positionRecord;
        if ((System.Object)p == null)
            return false;
        return position == p.position;
    }


    public bool Equals(positionRecord o)
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

public class SnakeSpawner : MonoBehaviour
{

    GameObject PlayerBox, StartPoint, breadcrumbBox, pathParent;

    List<positionRecord> pastPositions;

    public int snakelength = 6;

    int positionorder = 0;

    bool firstrun = true;

    // Start is called before the first frame update
    void Start()
    {
        StartPoint = GameObject.Find("StartPoint");

        PlayerBox = Instantiate(Resources.Load<GameObject>("Prefabs/Box"), StartPoint.transform.position, Quaternion.identity);

        PlayerBox.GetComponent<SpriteRenderer>().color = Color.black;

        PlayerBox.AddComponent<SnakeController>();

        breadcrumbBox = Resources.Load<GameObject>("Prefabs/Box");

        pastPositions = new List<positionRecord>();

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";

        drawTail(snakelength);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !((Input.GetMouseButtonDown(0)
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("a key was pressed " + Time.time);

            savePosition();

            drawTail(snakelength);

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
                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);

            }

        }

        if (firstrun)
        {

            //I don't have enough positions in the past positions list
            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
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
        foreach (positionRecord p in pastPositions)
        {
            // Debug.Log("Destroy tail" + pastPositions.Count);
            Destroy(p.BreadcrumbBox);
        }
    }

    void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = PlayerBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(PlayerBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, PlayerBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

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

        foreach (positionRecord p in pastPositions)
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
