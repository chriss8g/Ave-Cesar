using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleCards;

public class Viewer : Selectable
{
    public GameObject view;
    // Update is called once per frame
    void Update()
    {
        //informacion mostrada en los visualizadores al pasar sobre una carta
        if(IsHighlighted())
        {
            view.transform.localScale = new Vector3(1f, 1f, 0f);
            view.transform.GetChild(0).GetComponent<Image>().color = GetComponent<General>().Scriptable.color;
            view.transform.GetChild(1).GetComponent<Text>().text = GetComponent<General>().Scriptable.name;

            view.transform.GetChild(3).GetComponent<Text>().text = "";
            if((GetComponent<General>().Ghost is Hacker))
                view.transform.GetChild(3).GetComponent<Text>().text = "Capacidad: " + GetComponent<General>().Scriptable.capacity.ToString() + "\n\n";
            if((GetComponent<General>().Ghost is Algorithm))
                view.transform.GetChild(3).GetComponent<Text>().text = "Resistencia: " + GetComponent<General>().Scriptable.capacity.ToString() + "\n\n";

            view.transform.GetChild(3).GetComponent<Text>().text += ((GetComponent<General>().Ghost)).Description;
            view.transform.GetChild(4).GetComponent<Image>().sprite = GetComponent<General>().Scriptable.sprite;
        }
    
    }
}
