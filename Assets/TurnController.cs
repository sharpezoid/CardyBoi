using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -- control flow of turns
public class TurnController : MonoBehaviour
{
    GameController gc = null;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void DoTurn()
    {
        StartCoroutine(RunTurn());
    }

    IEnumerator RunTurn()
    {
        yield return StartCoroutine(RunStartOfTurn());

        yield return StartCoroutine(DrawCards());

        yield return StartCoroutine(PostDrawCards());

        yield return StartCoroutine(PlayCards());

        StartCoroutine(EndTurn());

        yield return null;
    }

    IEnumerator RunStartOfTurn()
    {
        

        yield return null;
    }

    IEnumerator DrawCards()
    {
        for (int hLoop = 0; hLoop < gc.squad.heroes.Count; hLoop++)
        {
            for (int cLoop = 0; cLoop < gc.squad.heroes[hLoop].maxDraw; cLoop++)
            {
                gc.squad.heroes[hLoop].DrawCard();

                gc.OnDrawAction();

                yield return new WaitForSeconds(0.1f);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    IEnumerator PostDrawCards()
    {

        yield return null;
    }

    public Queue<CardSceneObj> cardQueue = new Queue<CardSceneObj>();
    IEnumerator PlayCards()
    {
        // -- loop cards in card queue
        if (cardQueue.Count > 0)
        {
            CardSceneObj card = cardQueue.Dequeue();

            do
            {
                yield return card.PlayCard();
                if (cardQueue.Count > 0)
                {
                    card = cardQueue.Dequeue();
                }
                yield return null;
            }
            while (cardQueue.Count > 0);//|| !FightWon);

        }

        yield return null;
    }

    IEnumerator EndTurn()
    {
        yield return null;
    }
}