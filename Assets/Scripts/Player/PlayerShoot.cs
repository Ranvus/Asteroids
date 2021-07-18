using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    private float nextFire = 0f;

    [SerializeField] private AudioSource shootSound;

    private void Update()
    {
        if (MainMenu.Instance.controlText.text == "KEYBOARD" || PauseMenu.Instance.controlText.text == "KEYBOARD")
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire && !PauseMenu.Instance.isPaused)
            {
                Shoot();
            }
        }
        else if (MainMenu.Instance.controlText.text == "MOUSE + KEYBOARD" || PauseMenu.Instance.controlText.text == "MOUSE + KEYBOARD")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && Time.time > nextFire && !PauseMenu.Instance.isPaused)
            {
                Shoot();
            }
        }
    }

    internal void Shoot()
    {
        nextFire = Time.time + fireRate;
        ObjectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
        shootSound.Play();
    }
}
