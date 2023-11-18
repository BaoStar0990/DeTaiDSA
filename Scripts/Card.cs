using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
[System.Serializable]
public class Card : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite cardBack, cardFront;
    public bool isBack;

    public Deck_Game2 deck;
    private float offset;
    private GameObject enemyDel;
    // Start is called before the first frame update
    void Start()
    {
        deck = (GameObject.Find("Deck")).GetComponent<Deck_Game2>();
        offset = gameObject.transform.position.y;
        cardFront = spriteRenderer.sprite;
        isBack = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBack)
        {
            spriteRenderer.sprite = cardBack;
        }
        else
        {
            spriteRenderer.sprite = cardFront;
        }
    }

    public void OnMouseOver()
    {
        if (deck.playerDeck.Contains(gameObject))
        {
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            spr.color = Color.green;
        }
    }

    public void OnMouseExit()
    {
        if (deck.playerDeck.Contains(gameObject))
        {
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            spr.color = Color.white;
        }
    }
    public void OnMouseDown()
    {
        if(deck.playerDeck.Contains(gameObject))
        {
            GameObject enemyDel;

            foreach (var c in CardDatabase_2.cardDatabase_2)
            {
                if (cardFront == c.Value)
                {
                    Deck_Game2.playerScore += c.Key;
                    break;
                }
            }

            Card spriteRenderer = deck.enemyDeck[Random.Range(0, deck.enemyDeck.Count)].GetComponent<Card>();
            Sprite enemySprite = spriteRenderer.cardFront;

            foreach (var c in CardDatabase_2.cardDatabase_2)
            {
                if (enemySprite == c.Value)
                {
                    Deck_Game2.enemyScore += c.Key;
                    break;
                }
            }

            if(Deck_Game2.playerScore > Deck_Game2.enemyScore)
            {
                for (int i = 0; i < deck.playerDeck.Count; i++)
                {
                    SpriteRenderer spr = deck.playerDeck[i].GetComponent<SpriteRenderer>();
                    Sprite temp = spr.sprite;
                    if (temp == cardFront)
                    {
                        deck.playerDeck.RemoveAt(i);
                        Destroy(gameObject);
                        break;
                    }
                }

                deck.enemyDeck.Add(deck.deck[0]);
                deck.deck.RemoveAt(0);

            }
            else if(Deck_Game2.playerScore < Deck_Game2.enemyScore)
            {
                for (int i = 0; i < deck.enemyDeck.Count; i++)
                {
                    Card spr = deck.enemyDeck[i].GetComponent<Card>();
                    Sprite temp = spr.cardFront;
                    if (enemySprite == temp)
                    {
                        deck.enemyDeck.RemoveAt(i);
                        break;
                    }
                }
                deck.playerDeck.Add(deck.deck[0]);
                deck.deck.RemoveAt(0);
            }

            if (deck.playerDeck.Count > 0)
            {
                deck.SetUp(deck.playerDeck, deck.PosP, false);

            }

            if (deck.enemyDeck.Count > 0)
            {
                deck.SetUp(deck.enemyDeck, deck.PosE, true);
            }


        }

        deck.playerScoreText.text = Deck_Game2.playerScore.ToString();
        deck.enemyScoreText.text = Deck_Game2.enemyScore.ToString();
        Deck_Game2.playerScore = 0;
        Deck_Game2.enemyScore = 0;

        Result();
    }

    public void Result()
    {
        if (deck.deck.Count == 0)
        {
            if (deck.playerDeck.Count < deck.enemyDeck.Count)
                Deck_Game2.result = "YOU WIN";
            else if (deck.playerDeck.Count > deck.enemyDeck.Count)
                Deck_Game2.result = "YOU LOSE";
            else if (deck.playerDeck.Count == deck.enemyDeck.Count)
                Deck_Game2.result = "YOU DRAW";

            ObjectSwitch();

        }
        else if (deck.playerDeck.Count == 0)
        {
            Deck_Game2.result = "YOU WIN";
            ObjectSwitch();
        }
        else if (deck.enemyDeck.Count == 0)
        {
            Deck_Game2.result = "YOU LOSE";
            ObjectSwitch();
        }
    }

    public void ObjectSwitch()
    {
        deck.ResultObj.SetActive(true);
        deck.PlayAgain.SetActive(true);
        deck.shuffleBut.SetActive(false);
    }


}
