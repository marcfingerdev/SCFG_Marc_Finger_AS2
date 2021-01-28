using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    GameObject foodParticle;

    public float x_Start, y_Start;
    public int ColumnLength;
    public int RowLength;
    public int x_Space, y_Space;

    // Start is called before the first frame update
    void Start()
    {

        SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnFood()
    {
        for (int i = 0; i < ColumnLength * RowLength; i++)
        {
            Vector3 position;
            position = new Vector3(x_Start + (x_Space * (i % ColumnLength)), y_Start + (-y_Space * (i / ColumnLength)));
            //Instantiate(foodParticle, position, Quaternion.identity);

            Instantiate(Resources.Load<GameObject>("Prefabs/Hexa"), position, Quaternion.identity);
        }

    }

    
}
