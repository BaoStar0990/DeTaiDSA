using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour 
{
    public Deck deck;
    public static List<KeyValuePair<int,Sprite>> cardDatabase = new List<KeyValuePair<int,Sprite>>();
    public static List<KeyValuePair<int, Sprite>> Acards = new List<KeyValuePair<int,Sprite>>();
    private void Start()
    {
        GameObject deckFind = GameObject.Find("Deck");
        deck = deckFind.GetComponent<Deck>();
    }
    private void Awake()
    {
        cardDatabase.Clear();
        Acards.Clear();
        int key = 2;
        foreach(var i in deck.cards)
        {
            if(key < 10)
            {
                cardDatabase.Add(new KeyValuePair<int, Sprite>(key++, i.spriteRenderer.sprite));
            }
            else if(key >=10 && key < 14)
            {
                cardDatabase.Add(new KeyValuePair<int, Sprite>(10, i.spriteRenderer.sprite));
                key++;
            }
            else
            {
                cardDatabase.Add(new KeyValuePair<int, Sprite>(1, i.spriteRenderer.sprite));
                key = 2;
            }
        }

        foreach(var i in cardDatabase)
        {
            if (i.Key == 1)
                Acards.Add(i);
        }

    }
}
