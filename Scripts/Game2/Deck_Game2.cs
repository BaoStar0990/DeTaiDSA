using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Deck_Game2 : MonoBehaviour
{
    public List<GameObject> deck, playerDeck, enemyDeck;

    public GameObject PosP, PosE, Warning, ResultObj, PlayAgain, shuffleBut;

    public static string result;
    private bool isSplit;
    public static int playerScore, enemyScore;

    public Text playerScoreText, enemyScoreText, resultText;
    private void Awake()
    {
        PosP = GameObject.Find("PlayerCardPos");
        PosE = GameObject.Find("EnemyCardPos");
        isSplit = false;
        ResultObj = GameObject.Find("GameOver");
        resultText = ResultObj.GetComponent<Text>();
        ResultObj.SetActive(false);
        PlayAgain.SetActive(false);

    }

    void Start()
    {
        
        playerScore = 0;
        enemyScore = 0;

        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text =  enemyScore.ToString();
    }

    void Update()
    {
        resultText.text = result;
    }
    public void Origi()
    {
        
        if(!isSplit)
        {
            Warning.SetActive(false);
            shuffleBut.SetActive(false);
            Begin();
            isSplit=true;
        }

        deck[deck.Count - 1].transform.position = new Vector3(0, 0);
        for (int i = deck.Count - 2; i > 0; i--)
        {
            GameObject temp = deck[i + 1];
            deck[i].transform.position = new Vector3(temp.transform.position.x - 0.075f, temp.transform.position.y, temp.transform.position.z + 0.1f);
        }
    }

    public void Begin()
    {
        for (int i = 0; i < deck.Count && playerDeck.Count < 10; i++)
        {
            playerDeck.Add(deck[i]);
            deck.Remove(deck[i++]);
            enemyDeck.Add(deck[i]);
            deck.Remove(deck[i]);
        }

        SetUp(playerDeck, PosP, false);
        SetUp(enemyDeck, PosE, true);
    }

    public void SetUp(List<GameObject> hold,GameObject p, bool confirm)
    {
        hold[0].transform.position = p.transform.position;
        Card demo = hold[0].GetComponent<Card>();
        demo.isBack = confirm;

        for (int i = 1; i < hold.Count; i++)
        {
            GameObject temp = hold[i - 1];
            hold[i].transform.position = new Vector3(temp.transform.position.x - 0.4f, temp.transform.position.y, temp.transform.position.z + 0.4f);
            Card demo2 = hold[i].GetComponent<Card>();
            demo2.isBack = confirm;
        }
    }


}
