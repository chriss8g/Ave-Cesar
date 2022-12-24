using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using BattleCards;

public class CreateTxt : MonoBehaviour
{
    /// <summary>
    /// se activa cuando el formato no es correcto
    /// </summary>
    private static bool Correct = true;
    // Start is called before the first frame update
    void Start()
    {
        //mensaje de error oculto
        GameObject.FindGameObjectWithTag("Error").transform.localScale = new Vector3(0, 0, 0);

        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        string s = "";

        /// <summary>
        /// Recoje la informacion de los distintos campos y
        /// los formatea para guardarlos en un txt
        /// 
        /// </summary>
        /// <typeparam name="Text"></typeparam>
        /// <returns></returns>
        switch (GameObject.FindGameObjectWithTag("Select").transform.GetChild(0).GetComponent<Text>().text)
        {
            case "Hacker":
                s += "@Hacker@";
                s += GameObject.FindGameObjectWithTag("Hacker").transform.GetChild(0).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Hacker").transform.GetChild(1).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Hacker").transform.GetChild(2).GetComponent<InputField>().text;
                try
                {
                    Hacker pp = new Hacker(s);
                }
                catch (System.Exception)
                {
                    Correct = false;
                }
                break;
            case "Algorithm":
                s += "@Algorithm@";
                s += GameObject.FindGameObjectWithTag("Algorithm").transform.GetChild(0).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Algorithm").transform.GetChild(1).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Algorithm").transform.GetChild(2).GetComponent<InputField>().text;
                try
                {
                    Algorithm pp = new Algorithm(s);
                }
                catch (System.Exception)
                {
                    Correct = false;
                }
                break;
            case "Effect":
                switch (GameObject.FindGameObjectWithTag("Effect").transform.GetChild(4).GetChild(0).GetComponent<Text>().text)
                {
                    case "Sobre un campo":
                        s += "@EffectOverField@";
                        break;
                    case "Sobre una carta":
                        s += "@EffectOverCard@";
                        break;
                }

                s += GameObject.FindGameObjectWithTag("Effect").transform.GetChild(0).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Effect").transform.GetChild(1).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Effect").transform.GetChild(2).GetComponent<InputField>().text;
                s += "@";
                s += GameObject.FindGameObjectWithTag("Effect").transform.GetChild(3).GetComponent<InputField>().text;
                try
                {
                    switch (GameObject.FindGameObjectWithTag("Effect").transform.GetChild(4).GetChild(0).GetComponent<Text>().text)
                    {
                        case "Sobre un campo":
                            Effect pp = new EffectOverField(s);
                            break;
                        case "Sobre una carta":
                            pp = new EffectOverCard<Card>(s);
                            break;
                    }
                }
                catch (System.Exception)
                {
                    Correct = false;
                }
                break;
        }

        //muestra el mensaje d error si se puso algo mal
        if (!Correct)
        {

            GameObject.FindGameObjectWithTag("Error").GetComponent<Animator>().SetTrigger("error");
            Correct = true;
            return;
        }

        File.WriteAllText("./Assets/Cards/" + s.Split('@')[2] + ".txt", s);

        /// <summary>
        /// Pon en blanco los campos nuevamente
        /// </summary>
        /// <typeparam name="Text"></typeparam>
        /// <returns></returns>
        switch (GameObject.FindGameObjectWithTag("Select").transform.GetChild(0).GetComponent<Text>().text)
        {
            case "Hacker":
                GameObject.FindGameObjectWithTag("Hacker").transform.GetChild(0).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Hacker").transform.GetChild(1).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Hacker").transform.GetChild(2).GetComponent<InputField>().text = "";
                break;
            case "Algorithm":
                GameObject.FindGameObjectWithTag("Algorithm").transform.GetChild(0).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Algorithm").transform.GetChild(1).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Algorithm").transform.GetChild(2).GetComponent<InputField>().text = "";
                break;
            case "Effect":
                GameObject.FindGameObjectWithTag("Effect").transform.GetChild(0).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Effect").transform.GetChild(1).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Effect").transform.GetChild(2).GetComponent<InputField>().text = "";
                GameObject.FindGameObjectWithTag("Effect").transform.GetChild(3).GetComponent<InputField>().text = "";
                break;
        }
    }
}
