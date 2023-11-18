using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameLogic : MonoBehaviour
{
    public Deck_Game2 deck_game2;

    private void Awake()
    {
        deck_game2 = (GameObject.Find("Deck")).GetComponent<Deck_Game2>();
    }
    public void Shuffle()
    {
        for(int i = 0; i < deck_game2.deck.Count; i++)
        {
            int ran = Random.Range(0, deck_game2.deck.Count - 1);
            GameObject temp = deck_game2.deck[i];
            deck_game2.deck[i] = deck_game2.deck[ran];
            deck_game2.deck[ran] = temp;
        }

        deck_game2.Origi();
    }

}
