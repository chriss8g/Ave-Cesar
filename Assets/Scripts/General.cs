using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using BattleCards;

/// <summary>
/// Comunica los datos introducidos en mi Scriptable a los campod reales de mis cartas
/// </summary>
public class General : MonoBehaviour
{   //scriptable con las propiedades de las cartas
    public CardProperty Scriptable;
    //instancia de la carta que representa este objeto visual
    public Card Ghost;
    public GameObject name;
    public GameObject image;

    // Update is called once per frame
    void Update()
    {
        //informacion a mostrar en las cartas
        name.GetComponent<Text>().text = Scriptable.name;
        transform.GetChild(0).GetComponent<Image>().color = Scriptable.color;
        image.GetComponent<Image>().sprite = Scriptable.sprite;
    }
}
