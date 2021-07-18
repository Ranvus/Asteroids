using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private Rigidbody2D bulletRb;

    [SerializeField] private float bulletSpeed;

    public void OnObjectSpawn()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "UFO" || collision.gameObject.tag == "UFOBullet")
        {
            ObjectPooler.Instance.ReturnObject("Bullet", gameObject);
        }
    }
}
