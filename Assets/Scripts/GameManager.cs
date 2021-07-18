using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] public ParticleSystem playerExplosion;
    [SerializeField] public ParticleSystem asteroidExplosion;
    [SerializeField] public ParticleSystem ufoExplosion;

    [SerializeField] private float respawnTime = 3f;
    [SerializeField] private float immortalTime = 3f;

    [SerializeField] private int lives = 3;
    [SerializeField] private int scores = 0;

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoresText;

    [SerializeField] private AudioSource astLargeSound;
    [SerializeField] private AudioSource astMediumSound;
    [SerializeField] private AudioSource astSmallSound;
    [SerializeField] private AudioSource playerDeathSound;
    [SerializeField] private AudioSource ufoDestroySound;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        livesText.text = "Lives: " + lives;
        scoresText.text = "Scores: " + scores;
    }

    private void Update()
    {
        livesText.text = "Lives: " + lives;
        scoresText.text = "Scores: " + scores;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        asteroidExplosion.transform.position = asteroid.transform.position;
        asteroidExplosion.Play();

        if (asteroid.asteroidSize == 3)
        {
            scores += 20;
            astLargeSound.Play();
        }
        else if (asteroid.asteroidSize == 2)
        {
            scores += 50;
            astMediumSound.Play();
        }
        else if (asteroid.asteroidSize == 1)
        {
            scores += 100;
            astSmallSound.Play();
        }
    }

    public void UFODestroyed(UFO ufo)
    {
        ufoExplosion.transform.position = ufo.transform.position;
        ufoExplosion.Play();
        ufoDestroySound.Play();
        scores += 200;
    }

    public void PlayerDied()
    {
        playerExplosion.transform.position = player.transform.position;
        playerExplosion.Play();
        playerDeathSound.Play();

        lives--;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Immortal");
        player.gameObject.tag = "Immortal";
        player.isImmortal = true;
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), immortalTime);
    }

    private void TurnOnCollision()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.gameObject.tag = "Player";
        player.isImmortal = false;
    }

    private void GameOver()
    {
        PauseMenu.Instance.GameOverMenu();
    }
}
