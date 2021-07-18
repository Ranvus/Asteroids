using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public bool isImmortal;

    public Transform player;

    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameManager.Instance.Respawn();

        player = this.transform;
    }

    private void Update()
    {
        if (isImmortal)
        {
            anim.SetBool("IsImmortal", true);
        }
        else
        {
            anim.SetBool("IsImmortal", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "UFOBullet")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;

            this.gameObject.SetActive(false);

            GameManager.Instance.PlayerDied();
        }
    }
}
