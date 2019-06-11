using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    #region Components
    public GameObject caution;
    #endregion

    #region Attributes
    public float speed = 2.5f;
    private int direction;
    public float uprBound, lwrBound;
    private SpriteRenderer sprite;
    public GameObject barrel;
    #endregion

    #region States
    private bool hasCollided;
    private bool canShoot;
    public float shotShake;

    public GameObject bullet;
    private List<GameObject> pooledBullets;
    private bool canPivot;
    #endregion

    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        direction = 1;

        pooledBullets = new List<GameObject>();
        canShoot = true;
        canPivot = true;
	}
	

	void Update () {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if (!canPivot)
            return;

        if(transform.localPosition.x <= lwrBound)
        {
            direction = 1;
            sprite.flipX = false;
            canPivot = false;
            Invoke("resetPivot", 2);
        }
        else if(transform.localPosition.x >= uprBound)
        {
            direction =  - 1;
            sprite.flipX = true;
            canPivot = false;
            Invoke("resetPivot", 2);
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        bool present = obj.CompareTag("Player");

        if(present)
        {
            if (canShoot)
                shoot();
        }

        if (collision.gameObject.layer == 9)
        {
            if (!hasCollided)
            {
                direction = (direction < 0) ? 1 : -1;
                hasCollided = true;
                Invoke("resetCollision", 1);
            }
        }
    }


    private void resetCollision()
    {
        hasCollided = false;
    }


    private void shoot()
    {
        for(int i = 0; i < pooledBullets.Count; i++)
        {
            if(!pooledBullets[i].activeSelf)
            {
                setBullet(pooledBullets[i]);
                return;
            }
        }


        GameObject _bullet = Instantiate(bullet);
        _bullet.SetActive(false);
        pooledBullets.Add(_bullet);
        setBullet(_bullet);
    }

    private void setBullet(GameObject _bullet)
    {
        _bullet.GetComponent<BulletBehaviour>().setDirection((sprite.flipX) ? -1 : 1);
        _bullet.transform.position = barrel.transform.position;
        _bullet.SetActive(true);
        CameraShake.Instance.Shake();
        canShoot = false;
        Invoke("resetShoot", .75f);
    }

    private void resetShoot()
    {
        canShoot = true;
    }

    private void resetPivot()
    {
        canPivot = true;
    }
}
