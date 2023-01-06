using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour
{
    /// <summary>
    /// datos de la partida
    /// </summary>
    public static Game game;
    // Start is called before the first frame update
    void Start()
    {
        game = new Game();
    }
}
