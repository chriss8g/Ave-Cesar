using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using BattleCards;

public class VirtualPlayerVisual : MonoBehaviour
{
    public static void IA(int Number)
    {
        VirtualPlayer P1 = new VirtualPlayer();
        //intenta defenderse
        P1.InvokeAlgorithm(BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myHackers, BattleCards.Game.players[Number].myHand.Cards, BattleCards.Game.players[Number].myField.myAlgorithm);
        //juega una carta efecto o no al azar
        P1.InvokeEffect(BattleCards.Game.players[Number].myHand.Cards, BattleCards.Game.players[Number].myField.myEffects);
        //intenta atacar
        P1.InvokeHacker(BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myAlgorithm, BattleCards.Game.players[Number].myHand.Cards, BattleCards.Game.players[Number].myField.myHackers);

        //quita de la mano la carta que pueda haber jugado
        Transform Hand = Number == 0 ? GameObject.FindGameObjectWithTag("TotalHand").transform : GameObject.FindGameObjectWithTag("TotalHand2").transform;
        for (int i = 0; i < Hand.childCount; i++)
        {
            Card temp = BattleCards.Game.players[Number].myHand.Cards[i];
            if (Hand.GetChild(i).childCount == 1 && temp == null)
                Destroy(Hand.GetChild(i).GetChild(0).gameObject);
        }

        /// <summary>
        /// Crea la representacion visual de las cartas que puede haber jugado en los diferentes campos
        /// </summary>

        Transform SloA = Number == 0 ? GameObject.FindGameObjectWithTag("Slots").transform : GameObject.FindGameObjectWithTag("Slots2").transform;
        for (int i = 0; i < SloA.childCount; i++)
        {
            Card temp = BattleCards.Game.players[Number].myField.myAlgorithm[i];
            if (SloA.GetChild(i).GetChild(1).childCount == 1)
                continue;
            else if (temp != null)
            {
                GameObject pp = Create.CreateCard(temp);
                pp.transform.SetParent(SloA.GetChild(i).GetChild(1));
                SloA.GetChild(i).GetChild(1).GetChild(0).position = SloA.GetChild(i).GetChild(1).position;
            }
        }

        Transform SloE = GameObject.FindGameObjectWithTag("Effect").transform;
        for (int i = 0; i < SloE.childCount; i++)
        {
            Card temp = BattleCards.Game.players[Number].myField.myEffects[i];
            if (SloE.GetChild(i).childCount == 1)
                continue;
            else if (temp != null)
            {
                GameObject pp = Create.CreateCard(temp);
                pp.transform.SetParent(SloE.GetChild(i));
                SloE.GetChild(i).GetChild(0).position = SloE.GetChild(i).position;
            }
        }

        Transform SloH = Number == 1 ? GameObject.FindGameObjectWithTag("Slots").transform : GameObject.FindGameObjectWithTag("Slots2").transform;
        for (int i = 0; i < SloH.childCount; i++)
        {
            Card temp = BattleCards.Game.players[Number].myField.myHackers[i];
            if (SloH.GetChild(i).GetChild(0).childCount == 1)
                continue;
            else if (temp != null)
            {
                GameObject pp = Create.CreateCard(temp);
                pp.transform.SetParent(SloH.GetChild(i).GetChild(0));
                pp.transform.position = SloH.GetChild(i).GetChild(0).position;

            }
        }
    }

}
