using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleCards;

public class Game : MonoBehaviour
{
    void Update()
    {
        ChangeLife();
        //oculta el previsualizador si no se contradice esta orden
        GameObject.FindGameObjectWithTag("Viewer").transform.localScale = new Vector3(0, 0, 0);

        WinConditions();
    }
    public void ChangeLife()
    {
        //actualiza las barras de vida(el +70 de al final es para ajustar el canvas)
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().curHealth = BattleCards.Game.players[0].myLife.life + 70;
        GameObject.FindGameObjectWithTag("Player2").GetComponent<Health>().curHealth = BattleCards.Game.players[1].myLife.life + 70;
    }

    /// <summary>
    /// Llama a Chief que se encarga de robar cartas y actualiza la parte visual de la mano
    /// </summary>
    /// <param name="p">numero del jugador sobre el que se actua</param>
    public void UpdateHand(int p)
    {
        BattleCards.Game.players[p].myHand.Chief();
        Transform Hand = p == 0 ? GameObject.FindGameObjectWithTag("TotalHand").transform : GameObject.FindGameObjectWithTag("TotalHand2").transform;

        for (int i = 0; i < Hand.childCount; i++)
        {
            Card temp = BattleCards.Game.players[p].myHand.Cards[i];
            if (Hand.GetChild(i).childCount == 1)
            {
                Destroy(Hand.GetChild(i).GetChild(0).gameObject);
            }
            if (temp != null)
            {
                GameObject pp = Create.CreateCard(temp);
                pp.transform.SetParent(Hand.GetChild(i));
                pp.transform.position = Hand.GetChild(i).position;

            }
        }
    }
    /// <summary>
    /// Refresca las cartas en el campo del jugador
    /// </summary>
    /// <param name="p">indice del jugador</param>
    public static void UpdateField(int p)
    {
        Transform Algo = p == 0 ? GameObject.FindGameObjectWithTag("Slots").transform : GameObject.FindGameObjectWithTag("Slots2").transform;
        Transform Hack = p == 1 ? GameObject.FindGameObjectWithTag("Slots").transform : GameObject.FindGameObjectWithTag("Slots2").transform;

        for (int i = 0; i < Algo.childCount; i++)
        {
            Card temp = BattleCards.Game.players[p].myField.myAlgorithm[i];


            if (Algo.GetChild(i).GetChild(1).childCount == 1)
            {
                Destroy(Algo.GetChild(i).GetChild(1).GetChild(0).gameObject);
            }
            if (temp != null)
            {
                if ((temp as Algorithm).Resistance.life <= 0)
                {
                    temp = null;
                    continue;
                }
                GameObject pp = Create.CreateCard(temp);
                pp.transform.SetParent(Algo.GetChild(i).GetChild(1));
                pp.transform.position = Algo.GetChild(i).GetChild(1).position;
            }
        }
        for (int i = 0; i < Hack.childCount; i++)
        {
            Card temp = BattleCards.Game.players[p].myField.myHackers[i];
            if (Hack.GetChild(i).GetChild(0).childCount == 1)
            {
                Destroy(Hack.GetChild(i).GetChild(0).GetChild(0).gameObject);
            }
            if (temp != null)
            {
                GameObject pp = Create.CreateCard(temp);
                pp.transform.SetParent(Hack.GetChild(i).GetChild(0));
                pp.transform.position = Hack.GetChild(i).GetChild(0).position;
            }
        }
    }
    /// <summary>
    /// muestra la segnal de victoria cuando una de las vidas baja de cero
    /// </summary>
    private void WinConditions()
    {
        if (BattleCards.Game.players[1].myLife.life <= 0)
        {
            GameObject.FindGameObjectWithTag("Finish").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.FindGameObjectWithTag("WinP").transform.localScale = new Vector3(0.1129037f, 0.1129037f, 1f);
            Selector.value = 2;
        }
        if (BattleCards.Game.players[0].myLife.life <= 0)
        {
            GameObject.FindGameObjectWithTag("Finish").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.FindGameObjectWithTag("WinP2").transform.localScale = new Vector3(0.1129037f, 0.1129037f, 1f);
            Selector.value = 2;
        }
    }
}
