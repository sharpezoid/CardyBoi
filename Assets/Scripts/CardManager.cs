using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>
{
    public Queue<CardSceneObj> unusedCards = new Queue<CardSceneObj>();
    public int cardPool; // -- total cards of all our characters
    public int margin = 10; // -- plus some margin

    private void Start()
    {
        for(int sLoop = 0; sLoop < GameController.instance.squad.Count; sLoop++)
        {
            cardPool += GameController.instance.squad[sLoop].currentDeck.cards.Count;
        }
        cardPool += margin;

        for (int i = 0; i < cardPool; i++)
        {
            unusedCards.Enqueue(CreateCard());
        }
    }

    public CardSceneObj GetCard()
    {
        if (unusedCards.Count > 0)
        {
            return unusedCards.Dequeue();
        }
        return CreateCard();
    }

    public CardSceneObj CreateCard()
    {
        GameObject newCard = GameObject.Instantiate(GameController.instance.cardPrefab);
        return newCard.GetComponent<CardSceneObj>();
    }
}
