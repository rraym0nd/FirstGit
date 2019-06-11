using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int direction = 1;

    public float speed = 1.5f;

    void Start()
    {
        Invoke("kill", 2.5f);
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed;
    }

    public void setDirection(int dir)
    {
        direction = dir;
    }

    private void kill()
    {
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
