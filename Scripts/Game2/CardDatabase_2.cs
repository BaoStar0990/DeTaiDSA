using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase_2 : MonoBehaviour
{
    public Deck_Game2 deck2;
    public static List<KeyValuePair<int, Sprite>> cardDatabase_2 = new List<KeyValuePair<int, Sprite>>();
    private void Start()
    {
        GameObject deckFind = GameObject.Find("Deck");
        deck2 = deckFind.GetComponent<Deck_Game2>();
    }
    private void Awake()
    {
        cardDatabase_2.Clear();
        int key = 2;
        foreach (var i in deck2.deck)
        {
            SpriteRenderer temp = i.GetComponent<SpriteRenderer>();
            if (key < 10)
            {
                cardDatabase_2.Add(new KeyValuePair<int, Sprite>(key++, temp.sprite));
            }
            else if (key >= 10 && key < 14)
            {
                cardDatabase_2.Add(new KeyValuePair<int, Sprite>(10, temp.sprite));
                key++;
            }
            else
            {
                cardDatabase_2.Add(new KeyValuePair<int, Sprite>(1, temp.sprite));
                key = 2;
            }
        }

    }
}
