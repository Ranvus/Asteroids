using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBullet : MonoBehaviour, IPooledObject
{
    private Rigidbody2D ufoBulletRb;

    [SerializeField] private float ufoBulletSpeed;

    private Vector2 target;

    public void OnObjectSpawn()
    {
        ufoBulletRb = GetComponent<Rigidbody2D>();
        Vector2 moveDir = (Player.Instance.player.transform.position - transform.position).normalized * ufoBulletSpeed;
        ufoBulletRb.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Immortal" || collision.gameObject.tag == "Bullet")
        {
            ObjectPooler.Instance.ReturnObject("UFOBullet", gameObject);
        }
    }
}
