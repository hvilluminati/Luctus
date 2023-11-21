using Assets.Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour
{
    public static DeckManager instance;
    public Deck currentDeck;
    public int initialDeckAmount = 10;
    public Card[] cardTypes;

    public void Start()
    {
        currentDeck.cards.Clear();
        for (int i = 0; i < initialDeckAmount; i++)
        {
            Card card = cardTypes[Random.Range(0, cardTypes.Length)];
            currentDeck.cards.Add(card);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddRandomCard()
    {
        Card card = cardTypes[Random.Range(0, cardTypes.Length)];
        currentDeck.cards.Add(card);
    }
}
