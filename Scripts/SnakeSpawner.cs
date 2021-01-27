using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{

    GameObject PlayerBox;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBox = Instantiate(Resources.Load<GameObject>("Prefabs/Box"), new Vector3(0f, 0f), Quaternion.identity);

        PlayerBox.GetComponent<SpriteRenderer>().color = Color.black;

        PlayerBox.AddComponent<SnakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
