using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleCards;


/// <summary>
/// efecto de aparicion/desvanecimiento de las scenes
/// </summary>
public class transicion : MonoBehaviour
{
    private Animator _ani;
    // Start is called before the first frame update
    void Start()
    {
        _ani = GetComponent<Animator>();
    }

    public void LoadScene(string scene)
    {
        BattleCards.Game.players[0] = new Player();
        BattleCards.Game.players[1] = new Player();
        BattleCards.Game.turns = 0;

        StartCoroutine(Transicion(scene));
    }

    IEnumerator Transicion(string scene)
    {
        _ani.SetTrigger("salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
