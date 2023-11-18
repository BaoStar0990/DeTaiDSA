using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<Card> cards;
    public List<Sprite> sprites;
    public List<Transform> playerPos, enemyPos;

    public List<bool> isPut, isPutEnemy;

    public static int playerScore, enemyScore;

    public Text playerScoreText, enemyScoreText, resultText;

    public static string result;

    public GameObject drawBut, shuffleBut, stopBut, currentDeck, ReplayBut, EndingText1, EndingText2;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        enemyScore = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].spriteRenderer.sprite = sprites[i];
        }
        OrigiDeck();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
        resultText.text = result;
    }

    public void OrigiDeck()
    {
        float offset = 0;
        foreach (var card in cards)
        {
            card.transform.position = new Vector3(offset += 0.05f, 0, offset);
        }
    }

    public void Result()
    {
        drawBut.SetActive(false);
        stopBut.SetActive(false);
        shuffleBut.SetActive(false);
        ReplayBut.SetActive(true);
        EndingText1.SetActive(true);
        EndingText2.SetActive(true);

        if(playerScore < 16)
            result = "YOU LOSE";
        else if (enemyScore > 21 && playerScore > 21)
            result = "YOU DRAW";
        else if (enemyScore > 21)
            result = "YOU WIN";
        else if (playerScore > 21)
            result = "YOU LOSE";
        else if (isPut[4] == true && playerScore <= 21)
        {
            result = "YOU WIN";
        }
        else if (isPutEnemy[4] == true && enemyScore <= 21)
        {
            result = "YOU LOSE";
        }
        else if (isPutEnemy[4] == true && isPut[4] == true && playerScore == enemyScore)
        {
            result = "YOU DRAW";
        }
        else if (playerScore > enemyScore)
        {
            result = "YOU WIN";
        }
        else if (playerScore < enemyScore)
        {
            result = "YOU LOSE";
        }
        else if (playerScore == enemyScore)
        {
            result = "YOU DRAW";
        }
    }
}
