using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;


    void Awake()
    {
        //informacion para mostrar lo relacionado con la vida
        int i = transform.tag == "Player" ? 1 : 0;
        maxHealth = BattleCards.Game.players[i].myLife.life +70;
        curHealth = maxHealth;
    }
}