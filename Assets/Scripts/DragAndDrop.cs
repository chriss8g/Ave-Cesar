using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BattleCards;

public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    //objeto que se esta arrastrando
    public static GameObject itemDragging;

    //posicion inicial
    Vector3 starPosition;
    //padre inicial
    Transform starParent;
    //indice q ocupa en el conjunto de donde salio
    public static int starIndex;
    //Jugador que mueve la carta
    public static int CurrentPlayer;
    //nodo que sera padre mientras se arrastra
    Transform dragParent;
    //Esta variable se hace falsa cuando el jugador virtual 
    //juega para q el humano no pueda interferir
    public static bool Enable = true;

    // Start is called before the first frame update
    void Start()
    {
        dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        if (Enable)
        {
            itemDragging = gameObject;
            starPosition = transform.position;
            starParent = transform.parent;
            starIndex = transform.parent.GetSiblingIndex();
            transform.SetParent(dragParent);
            CurrentPlayer = Selector(starParent);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag0");
        if (Enable)
        {
            itemDragging = gameObject;
            //el numero es para ajustar la posicion local y global del mouse
            transform.position = Input.mousePosition / 2.18f;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        itemDragging = null;

        //a continuacion se establecen un gran grupo de condiciones
        //para que el usuario no pueda generar eventos para los cuales no esta autorizado
        bool Hacker;
        bool Algorithm;
        bool Effect = GetComponent<General>().Ghost is EffectOverField && transform.parent.parent.tag != "Effect";
        bool Hand;
        bool EffectOverCard = GetComponent<General>().Ghost is EffectOverCard<Card> && !((transform.parent.tag == "Algorithm" || transform.parent.tag == "Hacker") && (BattleCards.Game.players[CurrentPlayer].myField.myEffects[0] == null || BattleCards.Game.players[CurrentPlayer].myField.myEffects[1] == null));
        //Debug.Log(transform.parent.tag);
        if (CurrentPlayer == 0)
        {
            Hacker = GetComponent<General>().Ghost is Hacker && (transform.parent.tag != "Hacker" || transform.parent.parent.parent.tag != "Slots2");
            Algorithm = GetComponent<General>().Ghost is Algorithm && (transform.parent.tag != "Algorithm" || transform.parent.parent.parent.tag != "Slots");
            Hand = starParent.parent.tag != "TotalHand" ;//&& starParent.tag != transform.parent.tag;
        }
        else
        {
            Hacker = GetComponent<General>().Ghost is Hacker && (transform.parent.tag != "Hacker" || transform.parent.parent.parent.tag != "Slots");
            Algorithm = GetComponent<General>().Ghost is Algorithm && (transform.parent.tag != "Algorithm" || transform.parent.parent.parent.tag != "Slots2");
            Hand = starParent.parent.tag != "TotalHand2" ;//&& starParent.tag != transform.parent.tag;
        }

        //Si fallaran las premisas, el elemento que se arrastra retorna a su origen
        if (transform.parent == dragParent || Hand || Hacker || Algorithm || Effect || EffectOverCard || Player.Played == 0 || !Enable)
        {
            transform.position = starPosition;
            transform.SetParent(starParent);
        }
        else
        {
            //disminuye la cantidad de cartas ya jugadas.
            //note que este numero esta acotada en un turno
            Player.Played--;

            GameObject item = itemDragging;
            int i = CurrentPlayer;
            //una vez el usuario movio la carta, usa la informacion de este
            //evento para mover la carta en el codigo logico
            if (BattleCards.Game.players[i].myHand.Cards[starIndex] is Hacker)
            {
                BattleCards.Game.players[i].myHand.Cards.MoveCard<Hacker>(DragAndDrop.starIndex, BattleCards.Game.players[i].myField.myHackers, transform.parent.parent.GetSiblingIndex());
            }
            else if (BattleCards.Game.players[i].myHand.Cards[starIndex] is Algorithm)
            {
                BattleCards.Game.players[i].myHand.Cards.MoveCard<Algorithm>(DragAndDrop.starIndex, BattleCards.Game.players[i].myField.myAlgorithm, transform.parent.parent.GetSiblingIndex());
            }
            else if (BattleCards.Game.players[i].myHand.Cards[starIndex] is EffectOverField)
            {
                BattleCards.Game.players[i].myHand.Cards.MoveCard<Effect>(DragAndDrop.starIndex, BattleCards.Game.players[i].myField.myEffects, transform.parent.GetSiblingIndex());
            }
            else
            {
                (BattleCards.Game.players[i].myHand.Cards[starIndex] as EffectOverCard<Card>).Victim = transform.parent.GetChild(0).GetComponent<General>().Ghost;
                if (BattleCards.Game.players[i].myField.myEffects[0] == null)
                {
                    BattleCards.Game.players[i].myHand.Cards.MoveCard<Effect>(DragAndDrop.starIndex, BattleCards.Game.players[i].myField.myEffects, 0);
                    transform.SetParent(GameObject.FindGameObjectWithTag("Effect").transform.GetChild(0));
                    transform.position = GameObject.FindGameObjectWithTag("Effect").transform.GetChild(0).position;
                }
                else
                {
                    BattleCards.Game.players[i].myHand.Cards.MoveCard<Effect>(DragAndDrop.starIndex, BattleCards.Game.players[i].myField.myEffects, 1);
                    transform.SetParent(GameObject.FindGameObjectWithTag("Effect").transform.GetChild(1));
                    transform.position = GameObject.FindGameObjectWithTag("Effect").transform.GetChild(1).position;
                }

            }
        }

        starIndex = -1;

    }
    /// <summary>
    /// devuelve el indice del jugador actual
    /// </summary>
    /// <param name="starParent"></param>
    /// <returns></returns>
    public int Selector(Transform starParent)
    {
        if (starParent.parent.tag == "TotalHand" || (starParent.parent.parent.tag == "Slots" && starParent.tag == "Algorithm") || (starParent.parent.parent.tag == "Slots2" && starParent.tag == "Hacker"))
            return 0;
        if (starParent.parent.tag == "TotalHand2" || (starParent.parent.parent.tag == "Slots" && starParent.tag == "Hacker") || (starParent.parent.parent.tag == "Slots2" && starParent.tag == "Algorithm"))
            return 1;
        return -1;
    }
}
