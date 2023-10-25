using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class BallMovement : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody2D ballRigidbody;
    public float speed;
    private float currentSpeed;
    public float velocityMultiplier;
    public float baseVelocity = 2;
    public Player playerInGame;
    public GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip paddleSound;
    public AudioClip wallSound;

    System.Random random = new();
    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject;
        ballRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMatch(Player player)
    {
        currentSpeed = speed;
        Launch(player);
    }
    public void Restart(Player player)
    {
        StartMatch(player);
    }
    public void Launch(Player player)
    {
        ballRigidbody.position = new Vector2(0, 0);

        // Generate a random floating-point number between -1 and 1
        float yRandom = (float)(random.NextDouble() * 2) - 1;
        float xRandom;

        if (player == Player.PLAYER_1)
        {
            xRandom = (float)(random.NextDouble() * (-0.33f - (-1)) + (-1));
            UpdateVelocity(new Vector2(xRandom, yRandom).normalized);
        }
        else if (player == Player.PLAYER_2)
        {
            xRandom = (float)(random.NextDouble() * (1 - 0.33f) + 0.33f);
            UpdateVelocity(new Vector2(xRandom, yRandom).normalized);
        }
    }
    void UpdateVelocity(Vector2 direction)
    {
        currentSpeed += currentSpeed * velocityMultiplier;
        ballRigidbody.velocity = new Vector2(direction.x * currentSpeed, direction.y * currentSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            UpdateVelocity(new Vector2(
                ballRigidbody.velocity.x,
                (float)(ballRigidbody.velocity.y + random.NextDouble() * 6 - 3)
            ).normalized);
            gameManager.SetCurrentPlayerInTurn(collision.gameObject.GetComponent<PaddleMovement>().player);
            audioSource.clip = paddleSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = wallSound;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("LeftGoal"))
        {
            Score(Player.PLAYER_2);
        }
        else if (collider.gameObject.CompareTag("RightGoal"))
        {
            Score(Player.PLAYER_1);
        }
    }

    void Score(Player player)
    {
        gameManager.Score(player);
    }
}
