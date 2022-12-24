using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardProperty", menuName = "CardProperty")]
public class CardProperty : ScriptableObject
{
    public string name;

    public int capacity;
    public Color color;
    public Sprite sprite;
    public string Description;

}
