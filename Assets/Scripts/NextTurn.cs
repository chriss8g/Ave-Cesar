using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using BattleCards;

public class NextTurn : MonoBehaviour
{
    /// <summary>
    /// Que jugador es este?
    /// </summary>
    public int Number;


    // Start is called before the first frame update
    void Start()
    {
        Number = 1;
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
        TaskOnClick();

    }

    void TaskOnClick()
    {
        StartCoroutine("Next");
    }
    IEnumerator Next()
    {
        BattleCards.Game.NextTurn();
        UpdateHack();

        //renueva la cantidad de cartas que se pueden jugar en un turno
        Player.Played = 2;
        //cambia al indice del jugador actual
        Number = Number == 0 ? 1 : 0;
        //actualiza la variable para que este jugador pueda robar
        BattleCards.Game.PlayerNotHasStolen = true;

        ChangeTheVisual();

        //Activa las cartas efecto y las destruye
        for (int i = 0; i < BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects.Length(); i++)
        {
            if (BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i] != null)
            {
                if (BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i].Condition.Evaluate())
                {
                    if (BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i] is EffectOverCard<Card>)
                        (BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i] as EffectOverCard<Card>).Function((BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i] as EffectOverCard<Card>).Victim);
                    else
                        (BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i] as EffectOverField).Function();
                }
                BattleCards.Game.players[Number == 0 ? 1 : 0].myField.myEffects[i] = null;
                Destroy(GameObject.FindGameObjectWithTag("Effect").transform.GetChild(i).GetChild(0).gameObject);
            }
        }

        Attack();

        //refresca la imagen de los campos
        Game.UpdateField(Number);
        Game.UpdateField(Number == 0 ? 1 : 0);

        //Activa el jugador virtual
        if (Number == (Selector.value >= 0 ? Selector.value : Number))
        {
            //el humano no puede jugar y sus controles desaparecen
            DragAndDrop.Enable = false;
            GameObject.FindGameObjectWithTag("Next").transform.localScale = new Vector3(0, 0, 0);
            GameObject.FindGameObjectWithTag(Number == 0 ? "Deck" : "Deck2").transform.localScale = new Vector3(0, 0, 0);

            //Roba cartas
            yield return new WaitForSeconds(1.5f);
            Principal.game.UpdateHand(Number);
            //ejecuta movimientos
            yield return new WaitForSeconds(1.5f);
            VirtualPlayerVisual.IA(Number);
            yield return new WaitForSeconds(1.5f);

            //cambia las vistas y pasa el turno
            DragAndDrop.Enable = true;
            GameObject.FindGameObjectWithTag(Number == 0 ? "Deck" : "Deck2").transform.localScale = new Vector3(0.8064805f, 0.8064805f, 0.8064805f);
            GameObject.FindGameObjectWithTag("Next").transform.localScale = new Vector3(0.2664913f, 0.2664913f, 0);
            TaskOnClick();

        }
    }
    /// <summary>
    /// Hace q los hackers ataquen
    /// </summary>
    void Attack()
    {
        for (int i = 0; i < BattleCards.Game.players[Number].myField.myHackers.Length(); i++)
        {
            if (BattleCards.Game.players[Number].myField.myHackers[i] != null)
            {
                BattleCards.Game.players[Number].myField.myHackers[i].Attack(BattleCards.Game.players[Number == 0 ? 1 : 0], i);
            }
        }
    }
    /// <summary>
    /// desaparece y aparece los campos y manos segun corresponda
    /// </summary>
    void ChangeTheVisual()
    {
        if (Number == 0)
        {
            GameObject.FindGameObjectWithTag("TotalHand").transform.localScale = new Vector3(0.72649f, 0.72649f, 0.72649f);
            GameObject.FindGameObjectWithTag("TotalHand2").transform.localScale = new Vector3(0, 0, 0);
            GameObject.FindGameObjectWithTag("Deck").transform.localScale = new Vector3(0.8064805f, 0.8064805f, 0.8064805f);
            GameObject.FindGameObjectWithTag("Deck2").transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            GameObject.FindGameObjectWithTag("TotalHand2").transform.localScale = new Vector3(0.72649f, 0.72649f, 0.72649f);
            GameObject.FindGameObjectWithTag("TotalHand").transform.localScale = new Vector3(0, 0, 0);
            GameObject.FindGameObjectWithTag("Deck2").transform.localScale = new Vector3(0.8064805f, 0.8064805f, 0.8064805f);
            GameObject.FindGameObjectWithTag("Deck").transform.localScale = new Vector3(0, 0, 0);
        }
    }
    /// <summary>
    /// si la duracion de los hacker se vencio, destruyelos
    /// </summary>
    private void UpdateHack()
    {
        for (int i = 0; i < BattleCards.Game.players[Number].myField.myHackers.Length(); i++)
        {
            if (BattleCards.Game.players[Number].myField.myHackers[i] == null)
            {
                if (GameObject.FindGameObjectWithTag(Number == 1 ? "Slots" : "Slots2").transform.GetChild(i).GetChild(0).childCount == 1)
                {
                    Destroy(GameObject.FindGameObjectWithTag(Number == 1 ? "Slots" : "Slots2").transform.GetChild(i).GetChild(0).GetChild(0).gameObject);

                }
            }

        }
    }
}
