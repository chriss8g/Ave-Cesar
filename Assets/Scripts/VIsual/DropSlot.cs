using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BattleCards;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;
    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {
        //Asigna nuevas propiedades al elemento que se deja caer
        if (!item || DragAndDrop.itemDragging.GetComponent<General>().Ghost is EffectOverCard<Card>)
        {
            item = DragAndDrop.itemDragging;
            item.transform.SetParent(transform);
            item.transform.position = transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null && item.transform.parent != transform)
        {
            item = null;
        }
        if (transform.childCount == 1)//Actualiza la propiedad item con la carta q contiene
        {
            item = transform.GetChild(0).gameObject;
        }
    }
}
