using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D asteroidRb;
    private SpriteRenderer asteroidSr;

    [SerializeField] Sprite[] sprites;

    [SerializeField] private float maxThrust;

    [SerializeField] private float screenTop;
    [SerializeField] private float screenBottom;
    [SerializeField] private float screenLeft;
    [SerializeField] private float screenRight;

    [SerializeField] private GameObject asteroidMedium;
    [SerializeField] private GameObject asteroidSmall;

    public int asteroidSize;

    private void Awake()
    {
        asteroidRb = GetComponent<Rigidbody2D>();
        asteroidSr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        asteroidSr.sprite = sprites[Random.Range(0, sprites.Length)];

        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));

        asteroidRb.AddForce(thrust, ForceMode2D.Impulse);
    }

    private void Update()
    {
        Vector2 newPos = transform.position;

        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }

        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);

            GameManager.Instance.AsteroidDestroyed(this);

            if (asteroidSize == 3)
            {
                Instantiate(asteroidMedium, new Vector3(transform.position.x - .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, 45));
                Instantiate(asteroidMedium, new Vector3(transform.position.x - .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, 315));
            }
            else if (asteroidSize == 2)
            {
                Instantiate(asteroidSmall, new Vector3(transform.position.x - .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, 45));
                Instantiate(asteroidSmall, new Vector3(transform.position.x - .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, 315));
            }
            else if(asteroidSize == 1)
            {
                Destroy(gameObject);
            }
        }
    }

}
