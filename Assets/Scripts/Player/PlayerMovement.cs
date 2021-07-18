using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool isThrust;

    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float mouseRotSpeed;
    [SerializeField] private float thrustSpeed;

    [SerializeField] private float screenTop;
    [SerializeField] private float screenBottom;
    [SerializeField] private float screenLeft;
    [SerializeField] private float screenRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");

        if (MainMenu.Instance.controlText.text == "KEYBOARD" || PauseMenu.Instance.controlText.text == "KEYBOARD")
        {
            Rotate(transform, -xAxis * rotSpeed);
            isThrust = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        }
        else if (MainMenu.Instance.controlText.text == "MOUSE + KEYBOARD" || PauseMenu.Instance.controlText.text == "MOUSE + KEYBOARD")
        {
            MouseRotate(mouseRotSpeed);
            isThrust = Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        }

        anim.SetBool("IsThrust", isThrust);

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

    private void FixedUpdate()
    {
        if (isThrust)
        {
            ThrustForward(thrustSpeed);
        }

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;

        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    private void MouseRotate(float amount)
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, amount * Time.deltaTime);
    }
}
