using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{   
    bool play;
    // Start is called before the first frame update
    void Start()
    {
        play = true;
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);

    }
    //pause/play al sonido
    void TaskOnClick()
    {
        if(play)
            GetComponent<AudioSource>().Pause();
        else
            GetComponent<AudioSource>().Play();
        play = !play;
    }
}
