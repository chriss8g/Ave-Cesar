using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HAE : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        /// <summary>
        /// En dependencia de la opcion seleccionada se mostraran los 
        /// campos a llenar referentes a estos
        /// </summary>
        /// <typeparam name="Text"></typeparam>
        /// <returns></returns>
        switch (GameObject.FindGameObjectWithTag("Select").transform.GetChild(0).GetComponent<Text>().text)
        {
            case "Hacker":
                GameObject.FindGameObjectWithTag("Hacker").transform.localScale = new Vector3(1f, 1f, 1f);
                GameObject.FindGameObjectWithTag("Algorithm").transform.localScale = new Vector3(0, 0, 0);
                GameObject.FindGameObjectWithTag("Effect").transform.localScale = new Vector3(0, 0, 0);
                break;
            case "Algorithm":
                GameObject.FindGameObjectWithTag("Algorithm").transform.localScale = new Vector3(1f, 1f, 1f);
                GameObject.FindGameObjectWithTag("Hacker").transform.localScale = new Vector3(0, 0, 0);
                GameObject.FindGameObjectWithTag("Effect").transform.localScale = new Vector3(0, 0, 0);
                break;
            case "Effect":
                GameObject.FindGameObjectWithTag("Effect").transform.localScale = new Vector3(1f, 1f, 1f);
                GameObject.FindGameObjectWithTag("Algorithm").transform.localScale = new Vector3(0, 0, 0);
                GameObject.FindGameObjectWithTag("Hacker").transform.localScale = new Vector3(0, 0, 0);
                break;
        }
    }
}
