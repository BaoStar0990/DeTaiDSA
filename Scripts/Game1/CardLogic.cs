using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardLogic : MonoBehaviour
{
    public Deck deck;
    public handCard handCard;

    public GameObject drawBut, stopBut, shuffleBut, WarningText;
    private void Start()
    {
        GameObject deckFind = GameObject.Find("Deck");
        deck = deckFind.GetComponent<Deck>();

        GameObject deckFind2 = GameObject.Find("PlayerCard");
        handCard = deckFind2.GetComponent<handCard>();
    }
    public void Draw()
    {
        for(int i = 0;i<5;i++)
        {
            if (deck.isPut[i] == false)
            {
                Card temp = deck.cards[0];

                temp.isBack = false;
                handCard.playerCards.Add(temp);
                handCard.playerCards[i].transform.position = deck.playerPos[i].transform.position;

                foreach (var c in CardDatabase.cardDatabase)
                {
                    if (temp.cardFront == c.Value && Found(temp))
                    {
                        if (Deck.playerScore <= 10)
                            Deck.playerScore += 11;
                        else if(Deck.playerScore == 11)
                            Deck.playerScore = 10;
                        else
                            Deck.playerScore += 1;
                    }
                    else if (temp.cardFront == c.Value)
                    {
                        Deck.playerScore += c.Key;
                        break;
                    }
                }

                for (int j = 1; j < deck.cards.Count; j++)
                {
                    float offset = deck.cards[j].transform.position.x;
                    deck.cards[j].transform.position = new Vector3(offset -= 0.1f, 0, deck.cards[j].transform.position.z);
                }

                deck.isPut[i] = true;
                deck.cards.Remove(temp);

                deck.OrigiDeck();

                break;
            }
        }
    }

    public void Shuffle()
    {
        drawBut.SetActive(true);
        stopBut.SetActive(true);
        WarningText.SetActive(false);
        for (int j = 0; j < deck.cards.Count; j++)
        {
            int random = Random.Range(0, deck.cards.Count);
            Card temp = deck.cards[j];
            deck.cards[j] = deck.cards[random];
            deck.cards[random] = temp;
        }
        deck.OrigiDeck();
        shuffleBut.SetActive(false);
    }

    public void Stop()
    {
        while (Deck.enemyScore <= 16)
        {
            for (int i = 0; i < 5; i++)
            {
                if (deck.isPutEnemy[i] == false)
                {
                    Card temp = deck.cards[0];
                    temp.isBack = false;

                    handCard.enemyCards.Add(temp);
                    handCard.enemyCards[i].transform.position = deck.enemyPos[i].transform.position;

                    foreach (var c in CardDatabase.cardDatabase)
                    {
                        if (temp.cardFront == c.Value)
                        {
                            Deck.enemyScore += c.Key;
                            break;
                        }
                    }

                    for (int j = 1; j < deck.cards.Count; j++)
                    {
                        float offset = deck.cards[j].transform.position.x;
                        deck.cards[j].transform.position = new Vector3(offset -= 0.1f, 0, deck.cards[j].transform.position.z);
                    }

                    deck.isPutEnemy[i] = true;
                    deck.cards.Remove(temp);

                    deck.OrigiDeck();

                    break;
                }
            }
        }
        foreach (var i in deck.cards)
        {
            i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y, 100);
        }

        deck.Result();
    }
    
    private bool Found(Card x)
    {
        foreach(var i in CardDatabase.Acards)
        {
            if (x.cardFront == i.Value)
                return true;
        }
        return false;
    }
}
