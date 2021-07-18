using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    private Rigidbody2D ufoRb;

    [SerializeField] private float ufoSpeed;
    [SerializeField] private float shootRateMin;
    [SerializeField] private float shootRateMax;
    [SerializeField] private float shootRate;
    [SerializeField] private float nextShoot = 0f;

    [SerializeField] private GameObject ufoBulletPrefab;

    void Start()
    {
        ufoRb = GetComponent<Rigidbody2D>();

        if (UFOSpawner.Instance.spawnX == 8.3f)
        {
            ufoRb.AddForce(-transform.right * ufoSpeed, ForceMode2D.Impulse);
        }
        else
        {
            ufoRb.AddForce(transform.right * ufoSpeed, ForceMode2D.Impulse);
        }

    }

    void Update()
    {
        if (transform.position.x > 8.5f || transform.position.x < -8.5f)
        {
            Destroy(gameObject);
        }

        shootRate = Random.Range(shootRateMin, shootRateMax);

        shootRate -= Time.deltaTime;
        if (Time.time > nextShoot && Player.Instance.player.tag == "Player" && Player.Instance.player.gameObject.activeSelf)
        {
            UFOShoot();
            nextShoot = Time.time + shootRate; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            GameManager.Instance.UFODestroyed(this);
        }
    }

    private void UFOShoot()
    {
        ObjectPooler.Instance.SpawnFromPool("UFOBullet", transform.position, Quaternion.identity);
    }
}
