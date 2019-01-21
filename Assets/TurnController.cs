using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -- control flow of turns
public class TurnController : MonoSingleton<TurnController>
{
    GameController gc = null;

    public enum TurnState
    {
        None,
        StartTurn,
        DrawHand,
        PlayCards,
        WaitingOnCards,
        EndTurn
    }
    public TurnState turnState = TurnState.None;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();

        StartCoroutine(RunTurn());
    }

    //public void DoTurn()
    //{
    //    if (turnState == TurnState.None)
    //    {
    //        StartCoroutine(RunTurn());
    //    }
    //}

    IEnumerator RunTurn()
    {
        yield return StartCoroutine(RunStartOfTurn());

        yield return StartCoroutine(DrawCards());

        yield return StartCoroutine(PlayCards());

        yield return null;
    }

    IEnumerator RunStartOfTurn()
    {
        Debug.Log("Start Turn");

        turnState = TurnState.StartTurn;

        for(int hLoop = 0; hLoop < GameController.instance.squad.Count; hLoop++)
        {
            GameController.instance.squad[hLoop].hero.ResetTurn();
        }

        yield return null;
    }

    IEnumerator DrawCards()
    {
        Debug.Log("Drawing Cards...");

        turnState = TurnState.DrawHand;
        int totalDrawn = 0;
        for (int hLoop = 0; hLoop < gc.squad.Count; hLoop++)
        {
            int totalHeroDrawn = 0;
            for (int cLoop = 0; cLoop < gc.squad[hLoop].hero.maxDraw; cLoop++)
            {
                if (gc.squad[hLoop].DrawCard())
                {
                    totalHeroDrawn++;
                    totalDrawn++;
                    
                    gc.OnDrawAction();
                }

                yield return new WaitForSeconds(0.1f);
                yield return null;
            }

            Debug.Log("Hero finished drawing " + totalHeroDrawn + " cards");

            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("Done Drawing " + totalDrawn + " cards for " + gc.squad.Count + " heroes");

        yield return null;
    }

    public void AddCardToPlayedQueue(CardSceneObj card)
    {
        Debug.Log("Card Added To Play Queue : " + card.card.Name);

        cardQueue.Enqueue(card);
    }
    public Queue<CardSceneObj> cardQueue = new Queue<CardSceneObj>();
    IEnumerator PlayCards()
    {
        Debug.Log("Play Cards Start");

        turnState = TurnState.PlayCards;

        bool isPlaying = false;
        do
        {
            // -- loop cards in card queue
            if (cardQueue.Count > 0 && !isPlaying)
            {
                    CardSceneObj card = cardQueue.Dequeue();
                    isPlaying = true;
                    Debug.Log("Got Card To Play..." + " card : " + card.card.Name + " of " + cardQueue.Count );

                    yield return StartCoroutine(card.PlayCard());
                 
                    isPlaying = false;
                    yield return null;
            }
            yield return null;
        }
        while ( turnState == TurnState.PlayCards || cardQueue.Count > 0);

        Debug.Log("Play Cards End");

        StartCoroutine(RunEndTurn());

        yield return null;
    }

    public void EndTurn()
    {
        turnState = TurnState.WaitingOnCards;
    }

    public IEnumerator RunEndTurn()
    {
        Debug.Log("END TURN");

        OnScreenDebug.instance.Log(">>>>>>>>>  END TURN <<<<<<<<<<");

        turnState = TurnState.EndTurn;

        for(int hLoop = 0; hLoop < GameController.instance.squad.Count; hLoop++)
        {
            GameController.instance.squad[hLoop].hand.DiscardHand();
        }

        StartCoroutine(RunTurn());

        yield return null;
    }
}