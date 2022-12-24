using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleCards;

public class Create : MonoBehaviour
{
    /// <summary>
    /// A partir de una instancia de la clase card, creara su correspondiente visual
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public static GameObject CreateCard(Card temp)
    {
        //Crea carta a partir del Prefab
        GameObject pp = Instantiate(Resources.Load("item") as GameObject);

        //asigna el visualizador
        pp.GetComponent<Viewer>().view = GameObject.FindGameObjectWithTag("Viewer");

        //ScriptableObject para las propiedades de mi carta
        var Property = ScriptableObject.CreateInstance<CardProperty>();

        //mi carta visual tendra un componente Ghost que contiene la instancia d la card q representa
        pp.GetComponent<General>().Ghost = temp;

        //dota d las propiedades al scriptable
        AssociateLogicVisual(temp, Property);

        /// <summary>
        /// asocia el scriptable al prefab
        /// </summary>
        pp.GetComponent<General>().Scriptable = Property;
        return pp;
    }
    /// <summary>
    /// El nombre habla por si solo, mezcla ambos conceptos
    /// </summary>
    /// <param name="temp">instancia de una subclase de Card</param>
    /// <param name="Property"></param>
    private static void AssociateLogicVisual(Card temp, CardProperty Property)
    {
        if (temp is Hacker)
        {
            Hacker Tempo = (Hacker)temp;
            Property.name = Tempo.name;
            Property.capacity = Tempo.Capacity;
            Property.color = new Color(255, 0, 0, 0.5f);
        }
        else if (temp is Algorithm)
        {
            Algorithm Tempo = (Algorithm)temp;
            Property.name = Tempo.name;
            Property.capacity = Tempo.Resistance.life;
            Property.color = new Color(0, 0, 255, 0.5f);
        }
        else if (temp is EffectOverField)
        {
            EffectOverField Tempo = (EffectOverField)temp;
            Property.name = Tempo.name;
            Property.capacity = 0;
            Property.color = new Color(0, 255, 0, 0.5f);
        }
        else 
        {
            EffectOverCard<Card> Tempo = (EffectOverCard<Card>)temp;
            Property.name = Tempo.name;
            Property.capacity = 0;
            Property.color = new Color(0, 255, 0, 0.5f);
        }

        Property.Description = temp.Description;

        if (Resources.Load<Sprite>(Property.name) != null)
        {
            Property.sprite = Resources.Load<Sprite>(Property.name);
        }
        else
            Property.sprite = Resources.Load<Sprite>("Anonymus");
    }

}