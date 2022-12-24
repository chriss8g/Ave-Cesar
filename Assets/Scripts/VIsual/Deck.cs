using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using BattleCards;

public class Deck : MonoBehaviour
{
    /// <summary>
    /// indice del jugador
    /// </summary>
    private int Number;

    // Start is called before the first frame update
    void Start()
    {
        Number = transform.tag == "Deck" ? 0 : 1;
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }

    /// <summary>
    /// Roba cartas, a menos que ya hayas robado en el turno actual
    /// </summary>
    void TaskOnClick()
    {
        if (BattleCards.Game.PlayerNotHasStolen)
            Principal.game.UpdateHand(Number);
        BattleCards.Game.PlayerNotHasStolen = false;
    }

}
