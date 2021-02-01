using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBodyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            SnakeSpawner.snakelength++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            SnakeSpawner.snakelength = 0;
            EnemySpawner.snakelength = 0;
            Debug.Log("collided with wall");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "Finish")
        {
            if (SnakeSpawner.snakelength >= 6)
            {
                SnakeSpawner.snakelength = 0;
                EnemySpawner.snakelength = 0;
                Debug.Log("collided with finish");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            SnakeSpawner.snakelength = 0;
            EnemySpawner.snakelength = 0;
            Debug.Log("collided with enemy");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
