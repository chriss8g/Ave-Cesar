using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour
{
    string text = "La necesidad de mantener la información a salvo de los ojos curiosos no es nueva, es mucho más antigua de lo que quizás pueda usted llegar a imaginar.\n AVE CESAR hace alusión al cifrado César, el cual heredamos de la antigua Roma y cuyo uso se atribuye, como no podía ser de otra forma, a Julio César.\n Lo que aquí te presentamos es una batalla campal por la información y para ello contarás con un equipo tan aleatorio como preparado, de algoritmos y hackers (y alguna que otra cosilla especial).\n MUCHA SUERTE!!";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Play");
    }

    IEnumerator Play()
    {
        GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>().Play();

        GetComponent<Text>().text = "";

        for (int i = 0; i < text.Length; i++)
        {
            if(i%20 == 19)
                yield return new WaitForSeconds(0.3f);
            else if(i%20 == 7)
                yield return new WaitForSeconds(0.08f);
            else if(i%20 == 14)
                yield return new WaitForSeconds(0.12f);
            else
                yield return new WaitForSeconds(0.05f);

            GetComponent<Text>().text += text[i];
        }

        GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>().Pause();

        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("Next").GetComponent<transicion>().LoadScene("Menu");
    } 
}
