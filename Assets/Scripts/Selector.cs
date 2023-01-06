using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public static int value = 1;
    // Start is called before the first frame update
    void Start()
    {
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }
    //asigna el numero que designa la naturaleza de cada player
    //humano vs humano: 2
    //humano vs computadora: 1
    //computadora vs humano: 0
    //computadora vs computadora: -1
    void TaskOnClick()
    {
        if (GameObject.FindGameObjectWithTag("Select").transform.GetChild(0).GetComponent<Text>().text == "Humano")
        {
            if (GameObject.FindGameObjectWithTag("Select2").transform.GetChild(0).GetComponent<Text>().text == "Humano")
            {
                value = 2;
            }
            else
            {
                value = 1;
            }
        }
        else
        {
            if(GameObject.FindGameObjectWithTag("Select2").transform.GetChild(0).GetComponent<Text>().text == "Humano")
            {
                value = 0;
            }
            else
            {
                value = -1;
            }
        }
    }

}
