using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
   void Start()
    {
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }
    /// <summary>
    /// cierra la aplicacion
    /// </summary>
    void TaskOnClick()
    {
        Application.Quit();
    }
}
