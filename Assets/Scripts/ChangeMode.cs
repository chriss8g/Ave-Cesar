using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : MonoBehaviour
{
    void Start()
    {
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }
    /// <summary>
    /// Alterna el boton Humano/Computadora
    /// </summary>
    void TaskOnClick()
    {
        transform.GetChild(0).GetComponent<Text>().text = transform.GetChild(0).GetComponent<Text>().text == "Humano" ? "Computadora" : "Humano";
    }
}
