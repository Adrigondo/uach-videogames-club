using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class UIManager : MonoBehaviour
{

    [SerializedDictionary("Player", "TMP_Text")]
    public SerializedDictionary<Player, TMPro.TMP_Text> playerScores;
    public GameObject gameUI;
    public GameObject winScreenUI;
    public GameObject startScreenUI;
    public TMPro.TMP_Text winnerText;
    // Start is called before the first frame update

    public Tuple<Player,int> UpdateScore(Player player, int score)
    {
        playerScores[player].text = score.ToString();
        return new(player, score);
    }

    public void ShowWinner(Player player)
    {
        if (player == Player.PLAYER_1)
        {
            winnerText.text = "Jugador 1";
        }
        else
        {
            winnerText.text = "Jugador 2";
        }
        this.UpdateScreen(GameStatus.WIN_SCREEN);
    }

    public void UpdateScreen(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.START_SCREEN:
                {
                    startScreenUI.SetActive(true);
                    gameUI.SetActive(false);
                    winScreenUI.SetActive(false);
                    break;
                }
            case GameStatus.IN_PLAY:
                {
                    startScreenUI.SetActive(false);
                    gameUI.SetActive(true);
                    winScreenUI.SetActive(false);
                    break;
                }
            case GameStatus.WIN_SCREEN:
                {
                    startScreenUI.SetActive(false);
                    gameUI.SetActive(false);
                    winScreenUI.SetActive(true);
                    break;
                }
        }
    }

    void Restart()
    {
    }
}
