using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public int whoTurn; // 0 = X adn 1 = O
    public int turnCount; // count the number of turn played
    public GameObject[] turnIcons; //displays whos turn it is
    public Sprite[] playIcon; // 0 = X icon and 1 = O icon
    public Button[] tictactoeSpaces; //playable space for game
    public int[] markedSpaces; // ID's which space marked by player

    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText; // Hold the text component of the winner
    public GameObject[] winningLines; //Hold all the different lines for show that there a winner
    public int xPlayerScore;
    public int oPlayerScore;
    public TextMeshProUGUI xPlayerScoreText;
    public TextMeshProUGUI oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;

    private void Start()
    {
        GameSetup();
    }

    private void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);

        for(int i = 0; i < tictactoeSpaces.Length; i++) 
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].image.color = Color.white;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for(int i = 0;i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }

    }
    public void TicTacToeButton(int whichNumber)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;

        tictactoeSpaces[whichNumber].image.sprite = playIcon[whoTurn];

        if(whoTurn == 0) 
        {
            tictactoeSpaces[whichNumber].image.color = Color.red;
        }
        else if (whoTurn == 1)
        {
            tictactoeSpaces[whichNumber].image.color = Color.green;
        }

        tictactoeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoTurn+1;

        turnCount++;

        AudioManager.instance.PlaySFX("Button");

        if(turnCount > 4)
        {
            bool isWinner = WinnerCheck();

            if (turnCount == 9 && isWinner == false)
            {
                Draw();
            }
        }

        if(whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else 
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    private bool WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];

        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];

        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] {s1, s2, s3, s4, s5, s6, s7, s8};

        for(int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whoTurn + 1))
            {
                WinnerDisplay(i);
                AudioManager.instance.PlaySFX("Win");

                return true;
            }
        }
        return false;
    }

    private void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);

        if (whoTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X Won!";
        }
        else if(whoTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O Won!";
        }

        winningLines[indexIn].SetActive(true);
    }
    public void Rematch()
    {
        GameSetup();
        AudioManager.instance.PlaySFX("Switch");

        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }

        winnerPanel.SetActive(false);

        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
    }
    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;

        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
    }
    public void Draw()
    {
        winnerPanel.SetActive(true);
        AudioManager.instance.PlaySFX("Draw");
        winnerText.text = "Game Draw";
    }
    public void SwitchPlayer(int whichPlayer)
    {
        if(whichPlayer == 0)
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);

            AudioManager.instance.PlaySFX("Switch");
        }
        else if(whichPlayer == 1)
        {
            whoTurn = 1;
            turnIcons[1].SetActive(true);
            turnIcons[0].SetActive(false);

            AudioManager.instance.PlaySFX("Switch");
        }
    }
    public void Exit()
    {
        AudioManager.instance.PlaySFX("Switch");
        Application.Quit();
    }
}