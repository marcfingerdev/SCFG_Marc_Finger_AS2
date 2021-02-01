using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SnakeMovement();
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
            if(SnakeSpawner.snakelength >= 6) {
                SnakeSpawner.snakelength = 0;
                EnemySpawner.snakelength = 0;
                Debug.Log("collided with finish");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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

    void SnakeMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(1f, 0);
            checkBounds();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0);
            checkBounds();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1f);
            checkBounds();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, 1f);
            checkBounds();
        }
    }

    void checkBounds()
    {
        if ((transform.position.x < -(Camera.main.orthographicSize - 1)) || (transform.position.x > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize - 1)) || (transform.position.y > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }


    }

}
