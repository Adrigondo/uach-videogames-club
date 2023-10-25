using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using AYellowpaper.SerializedCollections;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class PlayerReferences
    {
        [SerializeField] public int score;
        [SerializeField] public PaddleMovement paddleMovement;
    }

    public UIManager UIManager;
    [SerializedDictionary("Player", "References")]
    public SerializedDictionary<Player, PlayerReferences> playerValues;
    public Player currentPlayerTurn = Player.PLAYER_1;
    public BallMovement ball;
    public GameStatus gameStatus=GameStatus.START_SCREEN;
    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip scoreSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartGame();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        gameStatus = GameStatus.START_SCREEN;
        UIManager.UpdateScreen(gameStatus);
    }
    public void StartMatch()
    {
        playerValues[Player.PLAYER_1].score = 0;
        playerValues[Player.PLAYER_2].score = 0;
        UIManager.UpdateScore(Player.PLAYER_1, 0);
        UIManager.UpdateScore(Player.PLAYER_2, 0);
        gameStatus = GameStatus.IN_PLAY;
        UIManager.UpdateScreen(gameStatus);
        StartBall();
    }

    public void StartBall()
    {

        int option = new System.Random().Next(2);
        if (option == 1)
        {
            currentPlayerTurn = Player.PLAYER_2;
        }
        else
        {
            currentPlayerTurn = Player.PLAYER_2;
        }
        ball.StartMatch(currentPlayerTurn);
    }

    public void SetCurrentPlayerInTurn(Player player)
    {

        currentPlayerTurn = player;
    }

    public Tuple<Player, int> Score(Player player)
    {
        audioSource.clip = scoreSound;
        audioSource.Play();
        SetCurrentPlayerInTurn(player);
        int score = playerValues[currentPlayerTurn].score;
        if (gameStatus == GameStatus.IN_PLAY)
        {
            score++;
            if (score == 3)
            {
                Win(player);
            }
            playerValues[currentPlayerTurn].score = score;
            UIManager.UpdateScore(currentPlayerTurn, score);
        }
        RestartMatch();
        return new(currentPlayerTurn, score);
    }
    public void Win(Player player)
    {
        audioSource.clip = winSound;
        audioSource.Play();
        gameStatus = GameStatus.WIN_SCREEN;
        UIManager.ShowWinner(player);

    }
    public void RestartMatch()
    {
        ball.Restart(currentPlayerTurn);
        playerValues[Player.PLAYER_1].paddleMovement.Restart();
        playerValues[Player.PLAYER_2].paddleMovement.Restart();
    }
}
