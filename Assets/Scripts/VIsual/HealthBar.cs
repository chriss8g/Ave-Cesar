using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// clase que gestiona el movimiento del slider que
/// representa la via del jugador
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    private void Start()
    {
        playerHealth = transform.parent.gameObject.GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }
    void Update()
    {
        //Debug.Log(playerHealth.curHealth);
        healthBar.value = playerHealth.curHealth;
        transform.GetChild(2).GetComponent<Text>().text = (BattleCards.Game.players[transform.tag == "Life" ? 0 : 1].myLife.life).ToString();
    }
}
